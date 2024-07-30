using System.Net.Sockets;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;

namespace OOT_AP_Client;

internal class Program
{
	private static void Main(string[] args)
	{
		var udpClient = new UdpClient();
		udpClient.Connect(hostname: "localhost", port: 55355);

		var retroarchMemoryService = new RetroarchMemoryService(udpClient);
		var locationCheckService = new LocationCheckService(retroarchMemoryService);
		var playerNameService = new PlayerNameService(retroarchMemoryService);

		var apSession = ArchipelagoSessionFactory.CreateSession("localhost");
		var loginResult = apSession.TryConnectAndLogin(
			game: "Ocarina of Time",
			name: "Player1",
			itemsHandlingFlags: ItemsHandlingFlags.RemoteItems
		);
		// var loginResult = apSession.TryConnectAndLogin("Ocarina of Time", "Player2", ItemsHandlingFlags.RemoteItems);

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

		playerNameService.WritePlayerName(index: 255, name: "APPlayer");
		Console.WriteLine("Player names written");

		while (true)
		{
			Task.Delay(500).Wait();
			var task = locationCheckService.GetAllCheckedLocationNames();
			task.Wait();
			var checkedLocationNames = task.Result;

			Console.WriteLine($"Checked locations: [{string.Join(separator: ", ", values: checkedLocationNames)}]");

			var checkedLocationIds = checkedLocationNames
				.Select(
					(locationName) => apSession.Locations.GetLocationIdFromName(
						game: "Ocarina of Time",
						locationName: locationName
					)
				)
				.ToArray();

			apSession.Locations.CompleteLocationChecks(checkedLocationIds);
		}
	}
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
