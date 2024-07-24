using System.Net.Sockets;

namespace OOT_AP_Client;

class Program
{
	static void Main(string[] args)
	{
		var udpClient = new UdpClient();
		udpClient.Connect("localhost", 55355);

		var retroarchMemoryService = new RetroarchMemoryService(udpClient);

		var task = retroarchMemoryService.Read16(0x11A604);
		task.Wait();
		
		Console.WriteLine(task.Result);

		var task2 = retroarchMemoryService.ReadByteArray(0x11F200, 4);
		task2.Wait();

		foreach (var num in task2.Result)
		{
			Console.Write($"{num:X} ");
		}
		Console.WriteLine();

		var task3 = retroarchMemoryService.Write16(0x11A604, 495);
		// var task3 = retroarchMemoryService.WriteByteArray(0x11A604, [0x01, 0xF4]);
		task3.Wait();
		Console.WriteLine(task3.Result);
	}
}

// This works for reading data, writing should be the same idea
// Can abstract this code into a class

// Can use a similar structure as the lua script:
// Generic methods for reading different kinds of checks (one class for these, static methods)
// Methods per region of the map that define all the checks for that region
// Central methods that handle checking the state every little bit, maybe every half second?
// Central method does stuff like deathlink, calling all location checks, and giving items (1 item at a time)

// Would be nice to get things such that the values from the lua script can be directly used or directly translated

// Setup I want to use for reading check states:
// have a big data structure somewhere that defines each check
// use an enum (or maybe I'll make a tiny Constant implementation) to say what type of check it is
// the rest should be a structure that is generic for any type of check
// may be as simple as this:
/* {
 *	checkType: enum,
 *  offset: string,
 *  bitToCheck: int
 * }
 */
 
// Overall TODO:
// Setup methods for reading and writing various sizes of data, as well as reading and writing byte[]s, this should abstract away the pointer swizzle and such
// Setup code for checking the state of a location
// Setup data structure and populate with all values from the lua script
// Setup general code that runs every half second, omit receiving items and deathlinks as a start, just have sending out locations
// Setup receiving items
// Setup deathlink
// Setup any auxiliary stuff like writing player names as needed