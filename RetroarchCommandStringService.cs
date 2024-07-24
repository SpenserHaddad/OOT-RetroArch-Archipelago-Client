using System.Globalization;

namespace OOT_AP_Client;

// TODO: add checks that look for error messages

// Response from Retroarch for reads looks like this: READ_CORE_MEMORY <address> 12 34 56...
// Response from Retroarch for writes looks like this: WRITE_CORE_MEMORY <address> <number of bytes written>
public static class RetroarchCommandStringService
{
	public static long ParseReadMemoryToLong(string receivedString, bool isBigEndian)
	{
		var bytes = ParseReadMemoryToArray(receivedString: receivedString, isBigEndian: isBigEndian);

		var outputNumber = default(long);

		var byteOffset = 0;
		foreach (var dataByte in bytes.Reverse())
		{
			outputNumber |= (uint)dataByte << byteOffset;

			byteOffset += 8;
		}

		return outputNumber;
	}

	public static byte[] ParseReadMemoryToArray(string receivedString, bool isBigEndian)
	{
		var byteStrings = receivedString.Split(' ').Skip(2);

		if (isBigEndian)
		{
			byteStrings = byteStrings.Reverse();
		}

		return byteStrings
			.Select((s) => byte.Parse(s: s, style: NumberStyles.HexNumber))
			.ToArray();
	}

	public static int ParseWriteMemoryBytesWritten(string receivedString)
	{
		var bytesWrittenString = receivedString.Split(' ')[2];

		return int.Parse(bytesWrittenString);
	}
}
