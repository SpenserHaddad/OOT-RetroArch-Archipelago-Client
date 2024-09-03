using OOT_AP_Client.OcarinaOfTime.Models;
using OOT_AP_Client.Services.Interfaces;

namespace OOT_AP_Client.OcarinaOfTime.Services;

public class GameModeService
{
	private readonly IMemoryService _memoryService;

	public GameModeService(IMemoryService memoryService)
	{
		_memoryService = memoryService;
	}

	public async Task<GameMode> GetCurrentGameMode()
	{
		var logoState = await GetLogoState();
		if (logoState is 0x802C5880 or 0)
		{
			return GameModes["N64 Logo"];
		}

		var mainState = await GetMainState();
		switch (mainState)
		{
			case 1:
				return GameModes["Title Screen"];
			case 2:
				return GameModes["File Select"];
		}

		var menuState = await GetMenuState();
		switch (menuState)
		{
			case 0:
			{
				var isLinkDying = await GetLinkIsDying();
				if (isLinkDying)
				{
					return GameModes["Dying"];
				}

				var subState = await GetSubState();
				if (subState == 4)
				{
					return GameModes["Cutscene"];
				}

				return GameModes["Normal Gameplay"];
			}
			case < 9 or 13 or 18 or 19:
				return GameModes["Paused"];
			case 9 or 0xB:
				return GameModes["Dying Menu Start"];
			default:
				return GameModes["Dead"];
		}
	}

	private async Task<byte> GetMainState()
	{
		const uint mainStateOffset = 0xA011B92F;

		return await _memoryService.Read8(mainStateOffset);
	}

	private async Task<byte> GetSubState()
	{
		const uint subStateOffset = 0xA011B933;

		return await _memoryService.Read8(subStateOffset);
	}

	private async Task<byte> GetMenuState()
	{
		const uint menuStateOffset = 0xA01D8DD5;

		return await _memoryService.Read8(menuStateOffset);
	}

	private async Task<uint> GetLogoState()
	{
		const uint logoStateOffset = 0xA011F200;

		return (uint)await _memoryService.Read32(logoStateOffset);
	}

	private async Task<bool> GetLinkIsDying()
	{
		const uint linkStateOffset = 0xA01DB09C;
		const uint linkHealthOffset = 0xA011A600;

		var linkState = await _memoryService.Read32(linkStateOffset);
		var linkHealth = await _memoryService.Read16(linkHealthOffset);

		return (linkState & 0x00000080) > 0 && linkHealth == 0;
	}

	// TODO: Change the names to be an enum
	private static readonly Dictionary<string, GameMode> GameModes = new GameMode[]
	{
		new() { Name = "N64 Logo", IsInGame = false },
		new() { Name = "Title Screen", IsInGame = false },
		new() { Name = "File Select", IsInGame = false },
		new() { Name = "Dying", IsInGame = true },
		new() { Name = "Cutscene", IsInGame = true },
		new() { Name = "Normal Gameplay", IsInGame = true },
		new() { Name = "Paused", IsInGame = true },
		new() { Name = "Dying Menu Start", IsInGame = false },
		new() { Name = "Dead", IsInGame = false },
	}.ToDictionary((gameMode) => gameMode.Name);
}
