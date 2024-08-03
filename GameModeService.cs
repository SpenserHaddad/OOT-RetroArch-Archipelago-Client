namespace OOT_AP_Client;

public class GameModeService
{
	private static readonly Dictionary<string, GameMode> GameModes = new GameMode[]
	{
		new(name: "N64 Logo", isInGame: false),
		new(name: "Title Screen", isInGame: false),
		new(name: "File Select", isInGame: false),
		new(name: "Dying", isInGame: true),
		new(name: "Cutscene", isInGame: true),
		new(name: "Normal Gameplay", isInGame: true),
		new(name: "Paused", isInGame: true),
		new(name: "Dying Menu Start", isInGame: false),
		new(name: "Dead", isInGame: false),
	}.ToDictionary((gameMode) => gameMode.Name);

	private readonly RetroarchMemoryService _retroarchMemoryService;

	public GameModeService(RetroarchMemoryService retroarchMemoryService)
	{
		_retroarchMemoryService = retroarchMemoryService;
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
			case < 9 or 13:
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

		return await _retroarchMemoryService.Read8(mainStateOffset);
	}

	private async Task<byte> GetSubState()
	{
		const uint subStateOffset = 0xA011B933;

		return await _retroarchMemoryService.Read8(subStateOffset);
	}

	private async Task<byte> GetMenuState()
	{
		const uint menuStateOffset = 0xA01D8DD5;

		return await _retroarchMemoryService.Read8(menuStateOffset);
	}

	private async Task<uint> GetLogoState()
	{
		const uint logoStateOffset = 0xA011F200;

		return (uint)await _retroarchMemoryService.Read32(logoStateOffset);
	}

	private async Task<bool> GetLinkIsDying()
	{
		const uint linkStateOffset = 0xA01DB09C;
		const uint linkHealthOffset = 0xA011A600;

		var linkState = await _retroarchMemoryService.Read32(linkStateOffset);
		var linkHealth = await _retroarchMemoryService.Read16(linkHealthOffset);

		return (linkState & 0x00000080) > 0 && linkHealth == 0;
	}
}

public record GameMode
{
	public GameMode(string name, bool isInGame)
	{
		Name = name;
		IsInGame = isInGame;
	}

	public string Name { get; }
	public bool IsInGame { get; }
}
