using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Newtonsoft.Json.Linq;
using OOT_AP_Client.OcarinaOfTime.Models;
using OOT_AP_Client.OcarinaOfTime.Services;
using OOT_AP_Client.Services;
using OOT_AP_Client.Services.Interfaces;

namespace OOT_AP_Client.OcarinaOfTime;

public class OOTClient
{
	private readonly ArchipelagoSession _apSession;
	private readonly DeathLinkService _archipelagoDeathLinkService;
	private readonly CollectibleCheckService _collectibleCheckService;

	private readonly OOTClientConnectionSettings _connectionSettings;
	private readonly CurrentSceneService _currentSceneService;
	private readonly GameCompleteService _gameCompleteService;
	private readonly GameModeService _gameModeService;
	private readonly LocationCheckService _locationCheckService;
	private readonly IMemoryService _memoryService;
	private readonly OOTClientDeathLinkService _ootClientDeathLinkService;
	private readonly PlayerNameService _playerNameService;
	private readonly ReceiveItemService _receiveItemService;

	public OOTClient()
	{
		_connectionSettings = PromptForConnectionSettings();

		var udpClient = new UdpClient();
		udpClient.Connect(hostname: _connectionSettings.RetroarchHostName, port: _connectionSettings.RetroarchPort);

		_memoryService = new RetroarchMemoryService(udpClient);
		_playerNameService = new PlayerNameService(_memoryService);
		_currentSceneService = new CurrentSceneService(_memoryService);
		_receiveItemService = new ReceiveItemService(
			memoryService: _memoryService,
			currentSceneService: _currentSceneService
		);
		_gameModeService = new GameModeService(_memoryService);
		_locationCheckService = new LocationCheckService(
			memoryService: _memoryService,
			gameModeService: _gameModeService
		);
		_collectibleCheckService = new CollectibleCheckService(_memoryService);
		_ootClientDeathLinkService = new OOTClientDeathLinkService(
			memoryService: _memoryService,
			gameModeService: _gameModeService,
			currentSceneService: _currentSceneService
		);
		_gameCompleteService = new GameCompleteService(_memoryService);

		_apSession = ArchipelagoSessionFactory.CreateSession(
			hostname: _connectionSettings.ArchipelagoHostName,
			port: _connectionSettings.ArchipelagoPort
		);
		var loginResult = _apSession.TryConnectAndLogin(
			game: "Ocarina of Time",
			name: _connectionSettings.SlotName,
			itemsHandlingFlags: ItemsHandlingFlags.RemoteItems
		);
		_archipelagoDeathLinkService = _apSession.CreateDeathLinkService();

		if (!loginResult.Successful)
		{
			var loginFailure = (LoginFailure)loginResult;
			throw new Exception(
				$"Connection to Archipelago failed. Error message(s):{Environment.NewLine}{string.Join(separator: Environment.NewLine, value: loginFailure.Errors)}{Environment.NewLine}"
			);
		}

		Console.WriteLine("Connected to Archipelago");
	}

	[DoesNotReturn]
	public async Task RunClient()
	{
		var slotData = await GetSlotData();

		await WritePlayerNames(apSession: _apSession, playerNameService: _playerNameService);

		// Setup DeathLink
		await _ootClientDeathLinkService.StoreDeathLinkEnabledFromMemory();
		var deathLinkEnabled = _ootClientDeathLinkService.DeathLinkEnabled;
		Console.WriteLine($"DeathLink {(deathLinkEnabled ? "is" : "is not")} enabled.");
		if (deathLinkEnabled)
		{
			_archipelagoDeathLinkService.EnableDeathLink();
			_archipelagoDeathLinkService.OnDeathLinkReceived += (_) =>
			{
				_ootClientDeathLinkService.ReceiveDeathLink();
				Console.WriteLine("Death link received");
			};
		}

		await _locationCheckService.InitializeMasterQuestHandling();
		await _locationCheckService.InitializeBigPoesRequired();

		var clientSideReceivedItemsCount = -1;

		var isGameCompletionSent = false;

		var wasPreviouslyInGame = false;

		while (true)
		{
			await Task.Delay(50);

			// Handle detecting resets and reinitialization
			var currentGameMode = await _gameModeService.GetCurrentGameMode();
			if (!currentGameMode.IsInGame)
			{
				wasPreviouslyInGame = false;
				continue;
			}

			if (!wasPreviouslyInGame)
			{
				wasPreviouslyInGame = true;
				await WritePlayerNames(apSession: _apSession, playerNameService: _playerNameService);
				clientSideReceivedItemsCount = -1;
			}

			// Handle completing location checks
			var checkedLocationIds = await GetAllCheckedLocationIds(slotData);
			await _apSession.Locations.CompleteLocationChecksAsync(checkedLocationIds);

			// Receive Items
			var gameReceivedItemsCount = await _receiveItemService.GetLocalReceivedItemIndex();
			if (gameReceivedItemsCount > clientSideReceivedItemsCount)
			{
				currentGameMode = await _gameModeService.GetCurrentGameMode();
				if (!currentGameMode.IsInGame)
				{
					continue;
				}

				if (_apSession.Items.Index > gameReceivedItemsCount)
				{
					clientSideReceivedItemsCount = gameReceivedItemsCount;

					var itemToReceive = _apSession.Items.AllItemsReceived[gameReceivedItemsCount];
					await _receiveItemService.ReceiveItem(itemToReceive);
				}
			}

			// Handle DeathLink
			var shouldSendDeathLink = await _ootClientDeathLinkService.ProcessDeathLink();
			if (shouldSendDeathLink)
			{
				var deathLink = new DeathLink(_connectionSettings.SlotName);
				_archipelagoDeathLinkService.SendDeathLink(deathLink);
				Console.WriteLine("Death link sent.");
			}

			// Handle Game Completion
			if (!isGameCompletionSent)
			{
				var isGameComplete = await _gameCompleteService.IsGameComplete();

				currentGameMode = await _gameModeService.GetCurrentGameMode();
				if (!currentGameMode.IsInGame)
				{
					continue;
				}

				if (isGameComplete)
				{
					_apSession.SetGoalAchieved();
					isGameCompletionSent = true;
					Console.WriteLine("Game completed");
				}
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

	private static OOTClientConnectionSettings PromptForConnectionSettings()
	{
		var cmdArgs = Environment.GetCommandLineArgs();

		string GetSettingFromArgOrPrompt(int argIndex, string prompt, string defaultValue) {
			if (cmdArgs.Length > argIndex) {
				return cmdArgs[argIndex];
			}
			else {
				Console.WriteLine($"{prompt}, default: {defaultValue}");
				var input = Console.ReadLine();
				if (string.IsNullOrWhiteSpace(input)) {
					return defaultValue;
				}
				else {
					return input;
				}
			}
		}

		string apHostname = GetSettingFromArgOrPrompt(1,"Enter the Archipelago Server Hostname", "archipelago.gg");
		string apPortString = GetSettingFromArgOrPrompt(2, "Enter the Archipelago Server port", "38281");
		int apPort = int.Parse(apPortString);
		string slotName = GetSettingFromArgOrPrompt(3, "Enter the Slot Name", "Player");
		string retroarchHostname = GetSettingFromArgOrPrompt(4, "Enter the Retroarch Hostname", "localhost");
		string retroarchPortString = GetSettingFromArgOrPrompt(5, "Enter the Retroarch Port", "55355");
		int retroarchPort = int.Parse(retroarchPortString);

		return new OOTClientConnectionSettings
		{
			ArchipelagoHostName = apHostname,
			ArchipelagoPort = apPort,
			SlotName = slotName,
			RetroarchHostName = retroarchHostname,
			RetroarchPort = retroarchPort,
		};
	}

	private async Task<OOTClientSlotData> GetSlotData()
	{
		var slotData = await _apSession.DataStorage.GetSlotDataAsync();

		// Defaults to false if not found to support OOT but it's just master quest water temple
		var scrubsanityEnabled = slotData.ContainsKey("shuffle_scrubs") && (long)slotData["shuffle_scrubs"] >= 1;

		// Reading from the 0x8000000 address range would be valid, it's the same memory as 0xA0000000, just keeping all accesses in 0xA0000000 for consistency
		var collectibleOverridesFlagsAddress
			= await _memoryService.Read32(
				0xA0400000 + Convert.ToUInt32(slotData["collectible_override_flags"])
			) - 0x80000000 + 0xA0000000;

		var collectibleFlagOffsets
			= SlotDataCollectableFlagOffsetsToArray(slotData["collectible_flag_offsets"] as JObject);

		return new OOTClientSlotData
		{
			ShuffleScrubs = scrubsanityEnabled,
			CollectibleOverridesFlagsAddress = collectibleOverridesFlagsAddress,
			CollectibleFlagOffsets = collectibleFlagOffsets,
		};

		static List<CollectibleFlagOffset> SlotDataCollectableFlagOffsetsToArray(
			JObject? slotDataCollectibleFlagOffsets
		)
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
	}

	private static async Task WritePlayerNames(ArchipelagoSession apSession, PlayerNameService playerNameService)
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

	private async Task<long[]> GetAllCheckedLocationIds(OOTClientSlotData slotData)
	{
		var checkedLocationNames = await _locationCheckService.GetAllCheckedLocationNames(slotData);

		var checkedLocationIds = checkedLocationNames
			.Select(
				(locationName) => _apSession.Locations.GetLocationIdFromName(
					game: "Ocarina of Time",
					locationName: locationName
				)
			);

		var checkedCollectibleIds = (await _collectibleCheckService.GetAllCheckedCollectibleIds(
			collectibleOverridesFlagAddress: slotData.CollectibleOverridesFlagsAddress,
			collectibleFlagOffsets: slotData.CollectibleFlagOffsets
		)).Select((id) => id);

		return checkedLocationIds.Concat(checkedCollectibleIds).ToArray();
	}
}
