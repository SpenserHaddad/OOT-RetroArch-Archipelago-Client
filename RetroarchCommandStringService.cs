using System.Globalization;
using System.Text;

namespace OOT_AP_Client;

// Response from Retroarch for reads looks like this: READ_CORE_MEMORY <address> 12 34 56...
// Response from Retroarch for writes looks like this: WRITE_CORE_MEMORY <address> <number of bytes written>
public static class RetroarchCommandStringService
{
	public static long ParseReadMemoryToLong(byte[] receivedBytes, bool isBigEndian)
	{
		var bytes = ParseReadMemoryToArray(receivedBytes: receivedBytes, isBigEndian: isBigEndian);

		var outputNumber = default(long);

		var byteOffset = 0;
		foreach (var dataByte in bytes.Reverse())
		{
			outputNumber |= (uint)dataByte << byteOffset;

			byteOffset += 8;
		}

		return outputNumber;
	}

	public static byte[] ParseReadMemoryToArray(byte[] receivedBytes, bool isBigEndian)
	{
		var receivedString = Encoding.UTF8.GetString(receivedBytes);

		var byteStrings = receivedString.Split(' ').Skip(2);

		if (isBigEndian)
		{
			byteStrings = byteStrings.Reverse();
		}

		return byteStrings
			.Select((s) => byte.Parse(s: s, style: NumberStyles.HexNumber))
			.ToArray();
	}
}
