using System.Globalization;

namespace OOT_AP_Client.Utils;

// TODO: add checks that look for error messages, the number after the address is -1 if it's an error, so detecting that case shouldn't be too hard

// Response from Retroarch for reads looks like this: READ_CORE_MEMORY <address> 12 34 56...
// Response from Retroarch for writes looks like this: WRITE_CORE_MEMORY <address> <number of bytes written>
public static class RetroarchCommandStringUtils
{
	public static long ParseAddress(string receivedString)
	{
		var address = receivedString.Trim().Split(' ').Skip(1).Take(1).Single();

		return long.Parse(address, style: NumberStyles.HexNumber);
	}

	public static int ParseNumberOfBytes(string receivedString)
	{
		var numberOfBytes = receivedString.Trim().Split(' ').Skip(2).Count();

		return numberOfBytes;
	}

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
		var byteStrings = receivedString.Trim().Split(' ').Skip(2);

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
		var bytesWrittenString = receivedString.Trim().Split(' ')[2];

		return int.Parse(bytesWrittenString);
	}
}
