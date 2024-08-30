using System.Net.Sockets;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Newtonsoft.Json.Linq;
using OOT_AP_Client.OcarinaOfTime.Models;
using OOT_AP_Client.OcarinaOfTime.Services;
using OOT_AP_Client.Services;
using DeathLinkService = OOT_AP_Client.OcarinaOfTime.Services.DeathLinkService;

Console.WriteLine("Enter the AP hostname, default: archipelago.gg");
var apHostname = Console.ReadLine();
if (string.IsNullOrWhiteSpace(apHostname))
{
	apHostname = "archipelago.gg";
}

Console.WriteLine("Enter the AP port, default: 38281");
var apPortString = Console.ReadLine();
var apPort = string.IsNullOrWhiteSpace(apPortString) ? 38281 : int.Parse(apPortString);

Console.WriteLine("Enter the Slot Name");
var slotName = Console.ReadLine();

var udpClient = new UdpClient();
udpClient.Connect(hostname: "localhost", port: 55355);

var retroarchMemoryService = new RetroarchMemoryService(udpClient);
var playerNameService = new PlayerNameService(retroarchMemoryService);
var currentSceneService = new CurrentSceneService(retroarchMemoryService);
var receiveItemService = new ReceiveItemService(
	memoryService: retroarchMemoryService,
	currentSceneService: currentSceneService
);
var gameModeService = new GameModeService(retroarchMemoryService);
var locationCheckService = new LocationCheckService(
	memoryService: retroarchMemoryService,
	gameModeService: gameModeService
);
var collectibleCheckService = new CollectibleCheckService(retroarchMemoryService);
var deathLinkService = new DeathLinkService(
	memoryService: retroarchMemoryService,
	gameModeService: gameModeService,
	currentSceneService: currentSceneService
);
var gameCompleteService = new GameCompleteService(retroarchMemoryService);

var apSession = ArchipelagoSessionFactory.CreateSession(hostname: apHostname, port: apPort);
var loginResult = apSession.TryConnectAndLogin(
	game: "Ocarina of Time",
	name: slotName,
	itemsHandlingFlags: ItemsHandlingFlags.RemoteItems
);

if (!loginResult.Successful)
{
	Console.WriteLine("Connection to Archipelago failed, exiting.");
	return;
}

Console.WriteLine("Connected to Archipelago");

var archipelagoDeathLinkService = apSession.CreateDeathLinkService();

var slotData = apSession.DataStorage.GetSlotData();
// Defaults to false if not found to support OOT but it's just master quest water temple
var scrubsanityEnabled = slotData.ContainsKey("shuffle_scrubs") && (long)slotData["shuffle_scrubs"] == 1;
var slotSettings = new SlotSettings(scrubsanityEnabled);
// Reading from the 0x8000000 address range would be valid, it's the same memory as 0xA0000000, just keeping all accesses in 0xA0000000 for consistency
var collectibleOverridesFlagsAddress
	= await retroarchMemoryService.Read32(
		0xA0400000 + Convert.ToUInt32(slotData["collectible_override_flags"])
	) - 0x80000000 + 0xA0000000;
var collectibleFlagOffsets = SlotDataCollectableFlagOffsetsToArray(slotData["collectible_flag_offsets"] as JObject);

await WritePlayerNames(apSession: apSession, playerNameService: playerNameService);

await deathLinkService.StoreDeathLinkEnabledFromMemory();
var deathLinkEnabled = deathLinkService.DeathLinkEnabled;
Console.WriteLine($"DeathLink {(deathLinkEnabled ? "is" : "is not")} enabled.");

await locationCheckService.InitializeMasterQuestHandling();
await locationCheckService.InitializeBigPoesRequired();

var clientSideReceivedItemsCount = -1;

var isGameCompletionSent = false;

if (deathLinkEnabled)
{
	archipelagoDeathLinkService.EnableDeathLink();
	archipelagoDeathLinkService.OnDeathLinkReceived += (_) =>
	{
		deathLinkService.ReceiveDeathLink();
		Console.WriteLine("Death link received");
	};
}

var wasPreviouslyInGame = false;

while (true)
{
	await Task.Delay(100);

	var currentGameMode = await gameModeService.GetCurrentGameMode();
	if (!currentGameMode.IsInGame)
	{
		wasPreviouslyInGame = false;
		continue;
	}

	if (!wasPreviouslyInGame)
	{
		wasPreviouslyInGame = true;
		await WritePlayerNames(apSession: apSession, playerNameService: playerNameService);
		clientSideReceivedItemsCount = -1;
	}

	var checkedLocationNames = await locationCheckService.GetAllCheckedLocationNames(slotSettings);

	var checkedLocationIds = checkedLocationNames
		.Select(
			(locationName) => apSession.Locations.GetLocationIdFromName(
				game: "Ocarina of Time",
				locationName: locationName
			)
		);

	var checkedCollectibleIds = (await collectibleCheckService.GetAllCheckedCollectibleIds(
		collectibleOverridesFlagAddress: collectibleOverridesFlagsAddress,
		collectibleFlagOffsets: collectibleFlagOffsets
	)).Select((id) => id);

	apSession.Locations.CompleteLocationChecks(checkedLocationIds.Concat(checkedCollectibleIds).ToArray());

	var gameReceivedItemsCount = await receiveItemService.GetLocalReceivedItemIndex();
	if (gameReceivedItemsCount > clientSideReceivedItemsCount)
	{
		currentGameMode = await gameModeService.GetCurrentGameMode();
		if (!currentGameMode.IsInGame)
		{
			continue;
		}

		if (apSession.Items.Index > gameReceivedItemsCount)
		{
			clientSideReceivedItemsCount = gameReceivedItemsCount;

			var itemToReceive = apSession.Items.AllItemsReceived[gameReceivedItemsCount];
			await receiveItemService.ReceiveItem(itemToReceive);
		}
	}

	await deathLinkService.ProcessDeathLink();

	if (deathLinkService.ShouldSendDeathLink())
	{
		var deathLink = new DeathLink(slotName);
		archipelagoDeathLinkService.SendDeathLink(deathLink);
		Console.WriteLine("Death link sent.");
	}

	if (!isGameCompletionSent)
	{
		var isGameComplete = await gameCompleteService.IsGameComplete();

		currentGameMode = await gameModeService.GetCurrentGameMode();
		if (!currentGameMode.IsInGame)
		{
			continue;
		}

		if (isGameComplete)
		{
			apSession.SetGoalAchieved();
			isGameCompletionSent = true;
			Console.WriteLine("Game completed");
		}
	}
}

// performance improvement idea:
// only check save context for locations on area changes, otherwise only use the temp context checks
// should do this skip inside the function for each check type, so that checks that don't have temp context still get checked for
// with how fast it is now, this would only be worth it for battery usage reasons

// idea for receiving local items:
// could have a sort of local database of checked locations, might want that anyway for performance reasons
// any location in the local save file that is checked would be in there, but if you make a new save then there could be locations checked in the multiworld that aren't marked as checked in the local database
// the idea would be that when processing the item queue, we can check against the local database, if the location is marked as checked there then that means we don't give the item, if it's not marked as checked then we do give the item
// this would avoid giving duplicate items but mean we can receive local items when making a new save file

static List<CollectibleFlagOffset> SlotDataCollectableFlagOffsetsToArray(JObject? slotDataCollectibleFlagOffsets)
{
	if (slotDataCollectibleFlagOffsets is null)
	{
		Console.WriteLine("SlotDataCollectibleFlagOffsets was null, this shouldn't happen!");
		return [];
	}

	var convertedCollectibleFlagOffsets = new List<CollectibleFlagOffset>(slotDataCollectibleFlagOffsets.Count);

	foreach (var flagOffsetData in slotDataCollectibleFlagOffsets)
	{
		// The slot data seems to always have a null key in it unless all collectible checks are enabled
		if (flagOffsetData.Key == "null")
		{
			continue;
		}

		var itemId = long.Parse(flagOffsetData.Key);

		var jArray = flagOffsetData.Value as JArray;
		if (jArray is null)
		{
			Console.WriteLine("Null JArray in collectible flag offsets, this shouldn't happen!");
			continue;
		}

		var offset = jArray[0].Value<long>();
		var flag = jArray[1].Value<long>();

		convertedCollectibleFlagOffsets.Add(
			new CollectibleFlagOffset
			{
				ItemId = itemId,
				Offset = offset,
				Flag = flag,
			}
		);
	}

	return convertedCollectibleFlagOffsets;
}

static async Task WritePlayerNames(ArchipelagoSession apSession, PlayerNameService playerNameService)
{
	var playerNames = apSession.Players.AllPlayers.Skip(1).Select(x => x.Name);
	var nameIndex = 1; // the names are 1 indexed, nothing is stored at index 0
	foreach (var name in playerNames)
	{
		if (nameIndex >= 255)
		{
			break;
		}

		await playerNameService.WritePlayerName(index: (byte)nameIndex, name: name);
		nameIndex++;
	}

	await playerNameService.WritePlayerName(index: 255, name: "APPlayer");
	Console.WriteLine("Player names written");
}
