namespace OOT_AP_Client.Utils;

public static class ByteUtils
{
	public static bool CheckBit(long memoryToCheck, byte bitToCheck)
	{
		return ((uint)(memoryToCheck & (1 << bitToCheck))) >= 1;
	}
}
