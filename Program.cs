using System.Net.Sockets;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using OOT_AP_Client;

var udpClient = new UdpClient();
udpClient.Connect(hostname: "localhost", port: 55355);

var retroarchMemoryService = new RetroarchMemoryService(udpClient);
var locationCheckService = new LocationCheckService(retroarchMemoryService);
var playerNameService = new PlayerNameService(retroarchMemoryService);
var currentSceneService = new CurrentSceneService(retroarchMemoryService);
var receiveItemService = new ReceiveItemService(
	retroarchMemoryService: retroarchMemoryService,
	currentSceneService: currentSceneService
);
var gameModeService = new GameModeService(retroarchMemoryService);

var apSession = ArchipelagoSessionFactory.CreateSession("localhost");
var loginResult = apSession.TryConnectAndLogin(
	game: "Ocarina of Time",
	name: "Player1",
	itemsHandlingFlags: ItemsHandlingFlags.RemoteItems
);
// var loginResult = apSession.TryConnectAndLogin(
// 	game: "Ocarina of Time",
// 	name: "Player2",
// 	itemsHandlingFlags: ItemsHandlingFlags.RemoteItems
// );

Console.WriteLine(loginResult.Successful ? "Connected to Archipelago" : "Failed to connect to Archipelago");

var playerNames = apSession.Players.AllPlayers.Skip(1).Select(x => x.Name);
var nameIndex = 1; // the names are 1 indexed, there's 8 bytes that never get used
foreach (var name in playerNames)
{
	if (nameIndex >= 255)
	{
		break;
	}

	var task = playerNameService.WritePlayerName(index: (byte)nameIndex, name: name);
	task.Wait();
	nameIndex++;
}

var applayerNameTask = playerNameService.WritePlayerName(index: 255, name: "APPlayer");
applayerNameTask.Wait();
Console.WriteLine("Player names written");

while (true)
{
	Task.Delay(500).Wait();
	var task = locationCheckService.GetAllCheckedLocationNames();
	task.Wait();
	var checkedLocationNames = task.Result;

	var checkedLocationIds = checkedLocationNames
		.Select(
			(locationName) => apSession.Locations.GetLocationIdFromName(
				game: "Ocarina of Time",
				locationName: locationName
			)
		)
		.ToArray();
	apSession.Locations.CompleteLocationChecks(checkedLocationIds);

	var localReceivedItemsCountTask = receiveItemService.GetLocalReceivedItemIndex();
	localReceivedItemsCountTask.Wait();
	if (apSession.Items.Index > localReceivedItemsCountTask.Result)
	{
		var itemToReceive = apSession.Items.AllItemsReceived[localReceivedItemsCountTask.Result];
		var receiveItemTask = receiveItemService.ReceiveItem(itemToReceive);
		receiveItemTask.Wait();
	}

	var gameModeTask = gameModeService.GetCurrentGameMode();
	gameModeTask.Wait();
	Console.WriteLine($"{{ {gameModeTask.Result.Name}, {gameModeTask.Result.IsInGame} }}");
}

// This works for reading data, writing should be the same idea
// Can abstract this code into a class

// Can use a similar structure as the lua script:
// Generic methods for reading different kinds of checks (one class for these, static methods)
// Methods per region of the map that define all the checks for that region
// Central methods that handle checking the state every little bit, maybe every half second?
// Central method does stuff like deathlink, calling all location checks, and giving items (1 item at a time)

// Overall TODO:
// DONE Setup methods for reading and writing various sizes of data, as well as reading and writing byte[]s, this should abstract away the pointer swizzle and such
// DONE (partially, only chests are setup) Setup code for checking the state of a location
// Setup data structure and populate with all values from the lua script
// DONE Setup general code that runs every half second, omit receiving items and deathlinks as a start, just have sending out locations
// Setup receiving items
// Setup deathlink
// Setup any auxiliary stuff like writing player names as needed

// Performance improvement idea:
// what if, instead of checking both temp context and save context every time, we only check temp context
// the temp context check could be a lot quicker, we could look at the scene first and then look at the id, so we could use a nested dictionary or something
// could construct that data structure on startup and store into a static variable, or process it in advance and serialize it, but it's probably not going to take long
// then, on a less frequent interval, we could check the full save context, maybe every 10 or 30 seconds
// could also grab save context data in a large chunk (eg all of the data for one area) and then process it
// save this for after v1

// idea for sending local items:
// could have a sort of local database of checked locations, might want that anyway for performance reasons
// any location in the local save file that is checked would be in there, but if you make a new save then there could be locations checked in the multiworld that aren't marked as checked in the local database
// the idea would be that when processing the item queue, we can check against the local database, if the location is marked as checked there then that means we don't give the item, if it's not marked as checked then we do give the item
// this would avoid giving duplicate items but mean we can receive local items when making a new save file
