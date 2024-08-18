using System.Collections.Immutable;
using System.Net.Sockets;
using System.Text;

namespace OOT_AP_Client.Services;

public record MemoryReadCommand
{
	public required long Address { get; init; }
	public required int NumberOfBytes { get; init; }
}

public class RetroarchMemoryService
{
	private readonly UdpClient _udpClient;

	public RetroarchMemoryService(UdpClient udpClient)
	{
		_udpClient = udpClient;
	}

	/// <summary>
	///     Reads the requested number of bytes from memory at the target address.
	///     Because of the swizzled memory, this supports a max of 4 bytes read at a time, or less if the address isn't 4n-1.
	/// </summary>
	public async Task<byte[]> ReadMemoryToByteArray(uint address, int numberOfBytes)
	{
		if (numberOfBytes > 4)
		{
			throw new ArgumentException("This method currently supports a max of 4 bytes read to get proper output.");
		}

		if (4 - address % 4 < numberOfBytes)
		{
			throw new ArgumentException("Requested bytes go beyond a single 4 byte chunk.");
		}

		var receivedBytes = await SendAndReceiveReadMemory(address: address, numberOfBytes: numberOfBytes);

		var dataFromMemory = RetroarchCommandStringService.ParseReadMemoryToArray(
			receivedString: receivedBytes,
			isBigEndian: true
		);

		return dataFromMemory;
	}

	public async Task<byte> Read8(long address)
	{
		return (byte)await ReadMemoryToLong(address: address, numberOfBytes: 1);
	}

	public async Task<short> Read16(long address)
	{
		return (short)await ReadMemoryToLong(address: address, numberOfBytes: 2);
	}

	public async Task<int> Read32(long address)
	{
		return (int)await ReadMemoryToLong(address: address, numberOfBytes: 4);
	}

	public async Task<Dictionary<long, long>> ReadMemoryToLongMulti(IEnumerable<MemoryReadCommand> readCommands)
	{
		var receivedStrings = await SendAndReceiveReadMemoryMulti(readCommands);

		var responses = new Dictionary<long, long>();
		foreach (var receivedString in receivedStrings)
		{
			var numberOfBytes = RetroarchCommandStringService.ParseNumberOfBytes(receivedString);
			var address = ConvertAddressFromN64(
				address: RetroarchCommandStringService.ParseAddress(receivedString),
				numberOfBytes: numberOfBytes
			);
			var data = RetroarchCommandStringService.ParseReadMemoryToLong(
				receivedString: receivedString,
				isBigEndian: true
			);
			responses.Add(key: address, value: data);
		}

		return responses;
	}

	public async Task<Dictionary<long, byte[]>> ReadMemoryToArrayMulti(IEnumerable<MemoryReadCommand> readCommands)
	{
		var receivedStrings = await SendAndReceiveReadMemoryMulti(readCommands);

		var responses = new Dictionary<long, byte[]>();
		foreach (var receivedString in receivedStrings)
		{
			var numberOfBytes = RetroarchCommandStringService.ParseNumberOfBytes(receivedString);
			var address = ConvertAddressFromN64(
				address: RetroarchCommandStringService.ParseAddress(receivedString),
				numberOfBytes: numberOfBytes
			);
			var data = RetroarchCommandStringService.ParseReadMemoryToArray(
				receivedString: receivedString,
				isBigEndian: true
			);
			responses.Add(key: address, value: data);
		}

		return responses;
	}

	// Input Array should be in little endian (e.g. 0x01F4 = 500 in base 10)
	// for now don't write more than 4 bytes at a time, this won't work if you do
	public async Task<int> WriteByteArray(long address, byte[] dataToWrite)
	{
		return await WriteMemory(address: address, dataToWrite: dataToWrite.Reverse().ToArray());
	}

	public async Task<int> Write8(long address, byte dataToWrite)
	{
		return await WriteMemory(
			address: address,
			dataToWrite: NumberToByteArray(number: dataToWrite, numberOfBytes: 1)
		);
	}

	public async Task<int> Write16(long address, short dataToWrite)
	{
		return await WriteMemory(
			address: address,
			dataToWrite: NumberToByteArray(number: dataToWrite, numberOfBytes: 2)
		);
	}

	public async Task<int> Write32(long address, int dataToWrite)
	{
		return await WriteMemory(
			address: address,
			dataToWrite: NumberToByteArray(number: dataToWrite, numberOfBytes: 4)
		);
	}

	// Need to massively rethink this service, right now the performance is abysmal
	// The issue is that the way this works, parallel stuff is impossible
	// It sends out a command, and then it takes the next value from the socket as the response
	// It would be better if it stored the command sent out, and then just waited for any response, and then we could get ours based on the data sent out

	// Need to think about this more, two interesting places where this is done: 
	// https://github.com/DigidragonZX/Archipelago/blob/82e3f970e7c9e802a82afac9b908f4868edfb530/worlds/_retroarch/socket.py
	// https://github.com/WeaponizedEmoticon/gamehook/blob/685645c3a5946a476882c4e0d2bdedf1597e6b58/src/GameHook.Domain/Drivers/RetroArchUdpPollingDriver.cs#L72

	// One idea for a better performing version would be to allow stuff to be completely async, where you send out a command and pass a callback
	// and then when the response comes in the callback gets run
	// That would work for speeding up a lot of small separate calls, although it adds a bunch of complexity

	private async Task<long> ReadMemoryToLong(long address, int numberOfBytes)
	{
		if (numberOfBytes > 8)
		{
			throw new ArgumentException(
				"Can't read more than 8 bytes per command with this method as it returns long, use the byte[] version instead"
			);
		}

		var receivedString = await SendAndReceiveReadMemory(address: address, numberOfBytes: numberOfBytes);

		var dataFromMemory = RetroarchCommandStringService.ParseReadMemoryToLong(
			receivedString: receivedString,
			isBigEndian: true
		);

		return dataFromMemory;
	}

	private async Task<string> SendAndReceiveReadMemory(long address, int numberOfBytes)
	{
		var convertedAddress = ConvertAddressToN64(address: address, numberOfBytes: numberOfBytes);

		_udpClient.Send(Encoding.UTF8.GetBytes($"READ_CORE_MEMORY {convertedAddress:X8} {numberOfBytes}"));

		var receivedBytes = (await _udpClient.ReceiveAsync()).Buffer;

		return Encoding.UTF8.GetString(receivedBytes);
	}

	private async Task<List<string>> SendAndReceiveReadMemoryMulti(IEnumerable<MemoryReadCommand> readCommands)
	{
		const int commandsPerIteration = 50;

		var inMemoryReadCommands = readCommands.ToImmutableArray();

		var stringBuilder = new StringBuilder();

		var receivedStrings = new List<string>();

		var commandsExecuted = 0;
		while (commandsExecuted < inMemoryReadCommands.Length)
		{
			foreach (var readCommand in inMemoryReadCommands.Skip(commandsExecuted).Take(commandsPerIteration))
			{
				if (readCommand.NumberOfBytes > 4)
				{
					throw new ArgumentException(
						"This method currently supports a max of 4 bytes read to get proper output."
					);
				}

				if (4 - readCommand.Address % 4 < readCommand.NumberOfBytes)
				{
					throw new ArgumentException("Requested bytes go beyond a single 4 byte chunk.");
				}

				stringBuilder.Append(
					$"READ_CORE_MEMORY {ConvertAddressToN64(address: readCommand.Address, numberOfBytes: readCommand.NumberOfBytes):X8} {readCommand.NumberOfBytes}\n"
				);
			}

			_udpClient.Send(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
			stringBuilder.Clear();

			var responseCounter = 0;
			while (responseCounter < Math.Min(commandsPerIteration, inMemoryReadCommands.Length - commandsExecuted))
			{
				var receivedBytes = (await _udpClient.ReceiveAsync()).Buffer;
				var receivedString = Encoding.UTF8.GetString(receivedBytes);
				receivedStrings.Add(receivedString);

				responseCounter++;
			}

			commandsExecuted += commandsPerIteration;
		}

		return receivedStrings;
	}

	private async Task<int> WriteMemory(long address, byte[] dataToWrite)
	{
		var receivedString = await SendAndReceiveWriteMemory(address: address, dataToWrite: dataToWrite);

		var bytesWritten = RetroarchCommandStringService.ParseWriteMemoryBytesWritten(receivedString);

		return bytesWritten;
	}

	private async Task<string> SendAndReceiveWriteMemory(long address, byte[] dataToWrite)
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

	private static long ConvertAddressToN64(long address, int numberOfBytes)
	{
		// The XOR 3 here is done because the memory within each 4 byte chunk is reversed, aka swizzled
		var translatedAddress = (address ^ 3) - (numberOfBytes - 1);

		return translatedAddress;
	}

	private static long ConvertAddressFromN64(long address, int numberOfBytes)
	{
		var translatedAddress = (address + (numberOfBytes - 1)) ^ 3;

		return translatedAddress;
	}

	// Outputs big endian byte array
	private static byte[] NumberToByteArray(long number, int numberOfBytes)
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
