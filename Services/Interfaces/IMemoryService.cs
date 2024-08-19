using OOT_AP_Client.Models;

namespace OOT_AP_Client.Services.Interfaces;

public interface IMemoryService
{
	Task<byte[]> ReadMemoryToByteArray(long address, int numberOfBytes);

	Task<byte> Read8(long address);

	Task<ushort> Read16(long address);

	Task<uint> Read32(long address);

	Task<Dictionary<long, long>> ReadMemoryToLongMulti(IEnumerable<MemoryReadCommand> readCommands);

	Task<Dictionary<long, byte[]>> ReadMemoryToArrayMulti(IEnumerable<MemoryReadCommand> readCommands);

	// Input Array should be in little endian (e.g. 0x01F4 = 500 in base 10)
	Task<int> WriteByteArray(long address, byte[] dataToWrite);

	Task Write8(long address, byte dataToWrite);

	Task Write16(long address, ushort dataToWrite);

	Task Write32(long address, uint dataToWrite);
}
