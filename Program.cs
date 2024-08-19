﻿using System.Net.Sockets;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Newtonsoft.Json.Linq;
using OOT_AP_Client.Models;
using OOT_AP_Client.Services;
using DeathLinkService = OOT_AP_Client.Services.DeathLinkService;

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
	retroarchMemoryService: retroarchMemoryService,
	currentSceneService: currentSceneService
);
var gameModeService = new GameModeService(retroarchMemoryService);
var locationCheckService = new LocationCheckService(
	retroarchMemoryService: retroarchMemoryService,
	gameModeService: gameModeService
);
var collectibleCheckService = new CollectibleCheckService(retroarchMemoryService);
var deathLinkService = new DeathLinkService(
	retroarchMemoryService: retroarchMemoryService,
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
var slotSettings = new SlotSettings((long)slotData["shuffle_scrubs"] == 1);
// Reading from the 0x8000000 address range would be valid, it's the same memory as 0xA0000000, just keeping all accesses in 0xA0000000 for consistency
var collectibleOverridesFlagsAddress
	= (uint)(await retroarchMemoryService.Read32(
		0xA0400000 + Convert.ToUInt32(slotData["collectible_override_flags"])
	) - 0x80000000 + 0xA0000000);
var collectibleFlagOffsets = SlotDataCollectableFlagOffsetsToArray((JObject)slotData["collectible_flag_offsets"]);

await WritePlayerNames(apSession: apSession, playerNameService: playerNameService);

await deathLinkService.StoreDeathLinkEnabledFromMemory();
var deathLinkEnabled = deathLinkService.DeathLinkEnabled;
Console.WriteLine($"DeathLink {(deathLinkEnabled ? "is" : "is not")} enabled.");

await locationCheckService.InitializeMasterQuestHandling();
await locationCheckService.InitializeBigPoesRequired();

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

	var localReceivedItemsCount = await receiveItemService.GetLocalReceivedItemIndex();

	currentGameMode = await gameModeService.GetCurrentGameMode();
	if (!currentGameMode.IsInGame)
	{
		continue;
	}

	if (apSession.Items.Index > localReceivedItemsCount)
	{
		var itemToReceive = apSession.Items.AllItemsReceived[localReceivedItemsCount];
		await receiveItemService.ReceiveItem(itemToReceive);
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

// Overall TODO:
// DONE Setup methods for reading and writing various sizes of data, as well as reading and writing byte[]s, this should abstract away the pointer swizzle and such
// DONE (partially, only chests are setup) Setup code for checking the state of a location
// DONE Setup general code that runs every half second, omit receiving items and deathlinks as a start, just have sending out locations
// DONE Setup receiving items
// DONE Setup deathlink
// DONE Setup writing player names
// DONE Setup game completion
// DONE Setup regular location checking and all locations
// DONE Setup collectible locations
// DONE Improve performance

// Performance improvement idea:
// what if, instead of checking both temp context and save context every time, we only check temp context
// the temp context check could be a lot quicker, we could look at the scene first and then look at the id, so we could use a nested dictionary or something
// could construct that data structure on startup and store into a static variable, or process it in advance and serialize it, but it's probably not going to take long
// then, on a less frequent interval, we could check the full save context, maybe every 10 or 30 seconds
// could also grab save context data in a large chunk (eg all of the data for one area) and then process it
// save this for after v1

// performance improvement idea:
// only check save context for locations on area changes, otherwise only use the temp context checks
// should do this skip inside the function for each check type, so that checks that don't have temp context still get checked for

// idea for sending local items:
// could have a sort of local database of checked locations, might want that anyway for performance reasons
// any location in the local save file that is checked would be in there, but if you make a new save then there could be locations checked in the multiworld that aren't marked as checked in the local database
// the idea would be that when processing the item queue, we can check against the local database, if the location is marked as checked there then that means we don't give the item, if it's not marked as checked then we do give the item
// this would avoid giving duplicate items but mean we can receive local items when making a new save file

static List<CollectibleFlagOffset> SlotDataCollectableFlagOffsetsToArray(JObject slotDataCollectibleFlagOffsets)
{
	var convertedCollectibleFlagOffsets = new List<CollectibleFlagOffset>(slotDataCollectibleFlagOffsets.Count);

	foreach (var flagOffsetData in slotDataCollectibleFlagOffsets)
	{
		var itemId = uint.Parse(flagOffsetData.Key);

		var jArray = flagOffsetData.Value as JArray;
		if (jArray is null)
		{
			Console.WriteLine("Null JArray in collectible flag offsets, this shouldn't happen!");
			continue;
		}

		var offset = jArray[0].Value<uint>();
		var flag = jArray[1].Value<uint>();

		convertedCollectibleFlagOffsets.Add(new CollectibleFlagOffset(itemId: itemId, offset: offset, flag: flag));
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
