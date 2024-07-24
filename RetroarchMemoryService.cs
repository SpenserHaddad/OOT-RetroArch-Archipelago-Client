using System.Net.Sockets;
using System.Text;

namespace OOT_AP_Client;

public class RetroarchMemoryService
{
	private readonly UdpClient _udpClient;

	public RetroarchMemoryService(UdpClient udpClient)
	{
		_udpClient = udpClient;
	}

	public async Task<byte[]> ReadByteArray(int address, int numberOfBytes)
	{
		var receivedBytes = await SendAndReceiveReadMemory(address: address, numberOfBytes: numberOfBytes);

		var dataFromMemory = RetroarchCommandStringService.ParseReadMemoryToArray(
			receivedBytes: receivedBytes,
			isBigEndian: true
		);

		return dataFromMemory;
	}

	public async Task<byte> Read8(int address)
	{
		return (byte)await ReadMemoryToLong(address: address, numberOfBytes: 1);
	}

	public async Task<short> Read16(int address)
	{
		return (short)await ReadMemoryToLong(address: address, numberOfBytes: 2);
	}

	public async Task<int> Read32(int address)
	{
		return (int)await ReadMemoryToLong(address: address, numberOfBytes: 4);
	}

	public async Task<long> Read64(int address)
	{
		return await ReadMemoryToLong(address: address, numberOfBytes: 8);
	}

	// Max of 8 bytes at a time (since it's reading into a long)
	private async Task<long> ReadMemoryToLong(int address, int numberOfBytes)
	{
		var receivedBytes = await SendAndReceiveReadMemory(address: address, numberOfBytes: numberOfBytes);

		var dataFromMemory = RetroarchCommandStringService.ParseReadMemoryToLong(
			receivedBytes: receivedBytes,
			isBigEndian: true
		);

		return dataFromMemory;
	}

	private async Task<byte[]> SendAndReceiveReadMemory(int address, int numberOfBytes)
	{
		// Not sure of why, Bizhawk does this XOR 3 on addresses before using them when the memory is "swizzled"
		// Also, this is reading big endian memory, which is why the offset is needed
		// Result of this math is being able to use memory addresses from the lua script directly
		var translatedAddress = (address | 3) - (numberOfBytes - 1);

		_udpClient.Send(Encoding.UTF8.GetBytes($"READ_CORE_MEMORY {translatedAddress:X8} {numberOfBytes}"));

		return (await _udpClient.ReceiveAsync()).Buffer;
	}
}
