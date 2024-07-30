using System.Text;

namespace OOT_AP_Client;

public class PlayerNameService
{
	private readonly RetroarchMemoryService _retroarchMemoryService;

	public PlayerNameService(RetroarchMemoryService retroarchMemoryService)
	{
		_retroarchMemoryService = retroarchMemoryService;
	}

	public async Task WritePlayerName(byte index, string name)
	{
		const uint namesAddress = 0x80400034;
		const byte maxPlayerNameBytes = 8;

		var nameToWriteAddress = namesAddress + (index * maxPlayerNameBytes);
		var bytesToWrite = new List<byte>(8);

		var asciiNameCharBytes = Encoding.ASCII.GetBytes(name);
		for (var i = 0; i < 8; i++)
		{
			var hasMoreCharacters = i < asciiNameCharBytes.Length;
			byte? charByte = hasMoreCharacters
				? asciiNameCharBytes[i]
				: null;

			switch (charByte)
			{
				case >= 0x30 and <= 0x39: // 0 to 9
					charByte -= 0x30;
					break;
				case >= 0x41 and <= 0x5A: // A to Z
					charByte += 0x6A;
					break;
				case >= 0x61 and <= 0x7A: // a to z
					charByte += 0x64;
					break;
				case 0x2E: // .
					charByte = 0xEA;
					break;
				case 0x2D: // -
					charByte = 0xE4;
					break;
				case 0x20: // <space>
					charByte = 0xDF;
					break;
				default:
					charByte = null;
					break;
			}

			if (charByte is null && hasMoreCharacters)
			{
				continue;
			}

			bytesToWrite.Add(charByte ?? 0xDF);
		}

		await _retroarchMemoryService.WriteByteArray(
			address: (uint)nameToWriteAddress,
			dataToWrite: bytesToWrite.Take(4).ToArray()
		);
		await _retroarchMemoryService.WriteByteArray(
			address: (uint)nameToWriteAddress + 4,
			dataToWrite: bytesToWrite.Skip(4).Take(4).ToArray()
		);
	}
}
