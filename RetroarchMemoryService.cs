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

	public async Task<byte[]> ReadByteArray(uint address, int numberOfBytes)
	{
		var receivedBytes = await SendAndReceiveReadMemory(address: address, numberOfBytes: numberOfBytes);

		var dataFromMemory = RetroarchCommandStringService.ParseReadMemoryToArray(
			receivedString: receivedBytes,
			isBigEndian: true
		);

		return dataFromMemory;
	}

	public async Task<byte> Read8(uint address)
	{
		return (byte)await ReadMemoryToLong(address: address, numberOfBytes: 1);
	}

	public async Task<short> Read16(uint address)
	{
		return (short)await ReadMemoryToLong(address: address, numberOfBytes: 2);
	}

	public async Task<int> Read32(uint address)
	{
		return (int)await ReadMemoryToLong(address: address, numberOfBytes: 4);
	}

	public async Task<long> Read64(uint address)
	{
		return await ReadMemoryToLong(address: address, numberOfBytes: 8);
	}

	// Input Array should be in little endian (eg 0x01F4 = 500 in base 10)
	public async Task<int> WriteByteArray(uint address, byte[] dataToWrite)
	{
		return await WriteMemory(address: address, dataToWrite: dataToWrite.Reverse().ToArray());
	}

	public async Task<int> Write8(uint address, byte dataToWrite)
	{
		return await WriteMemory(
			address: address,
			dataToWrite: NumberToByteArray(number: dataToWrite, numberOfBytes: 1)
		);
	}

	public async Task<int> Write16(uint address, short dataToWrite)
	{
		return await WriteMemory(
			address: address,
			dataToWrite: NumberToByteArray(number: dataToWrite, numberOfBytes: 2)
		);
	}

	public async Task<int> Write32(uint address, int dataToWrite)
	{
		return await WriteMemory(
			address: address,
			dataToWrite: NumberToByteArray(number: dataToWrite, numberOfBytes: 4)
		);
	}

	public async Task<int> Write64(uint address, long dataToWrite)
	{
		return await WriteMemory(
			address: address,
			dataToWrite: NumberToByteArray(number: dataToWrite, numberOfBytes: 8)
		);
	}

	// Max of 8 bytes at a time (since it's reading into a long)
	private async Task<long> ReadMemoryToLong(uint address, int numberOfBytes)
	{
		var receivedString = await SendAndReceiveReadMemory(address: address, numberOfBytes: numberOfBytes);

		var dataFromMemory = RetroarchCommandStringService.ParseReadMemoryToLong(
			receivedString: receivedString,
			isBigEndian: true
		);

		return dataFromMemory;
	}

	// TODO: might want a timeout waiting for the response here, need to look into why but i think it sometimes gets stuck waiting
	private async Task<string> SendAndReceiveReadMemory(uint address, int numberOfBytes)
	{
		var convertedAddress = ConvertAddressToN64(address: address, numberOfBytes: numberOfBytes);

		_udpClient.Send(Encoding.UTF8.GetBytes($"READ_CORE_MEMORY {convertedAddress:X8} {numberOfBytes}"));

		var receivedBytes = (await _udpClient.ReceiveAsync()).Buffer;

		return Encoding.UTF8.GetString(receivedBytes);
	}

	private async Task<int> WriteMemory(uint address, byte[] dataToWrite)
	{
		var receivedString = await SendAndReceiveWriteMemory(address: address, dataToWrite: dataToWrite);

		var bytesWritten = RetroarchCommandStringService.ParseWriteMemoryBytesWritten(receivedString);

		return bytesWritten;
	}

	private async Task<string> SendAndReceiveWriteMemory(uint address, byte[] dataToWrite)
	{
		var convertedAddress = ConvertAddressToN64(address: address, numberOfBytes: dataToWrite.Length);

		var dataToWriteString = string.Join(
			separator: ' ',
			values: dataToWrite.Select((b) => string.Format(format: "{0:X2}", arg0: b))
		);

		var str = $"WRITE_CORE_MEMORY {convertedAddress:X8} {dataToWriteString}";
		var bytes = Encoding.UTF8.GetBytes(str);
		_udpClient.Send(bytes);

		var receivedBytes = (await _udpClient.ReceiveAsync()).Buffer;

		return Encoding.UTF8.GetString(receivedBytes);
	}

	private static uint ConvertAddressToN64(uint address, int numberOfBytes)
	{
		// Not sure of why, Bizhawk does this XOR 3 on addresses before using them when the memory is "swizzled"
		// Also, this is reading big endian memory, which is why the offset is needed
		// Result of this math is being able to use memory addresses from the lua script directly
		var translatedAddress = (uint)((address | 3) - (numberOfBytes - 1));

		return translatedAddress;
	}

	// Outputs big endian byte array
	private byte[] NumberToByteArray(long number, int numberOfBytes)
	{
		var outputByteArray = new byte[numberOfBytes];

		var offset = 8 * (numberOfBytes - 1);
		for (var i = 0; i < numberOfBytes; i++)
		{
			outputByteArray[numberOfBytes - i - 1] = (byte)(number >> offset);

			offset -= 8;
		}

		return outputByteArray.ToArray();
	}
}
