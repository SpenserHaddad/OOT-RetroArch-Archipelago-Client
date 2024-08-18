namespace OOT_AP_Client.Services;

public class DeathLinkService
{
	private readonly RetroarchMemoryService _retroarchMemoryService;
	private readonly CurrentSceneService _currentSceneService;
	private readonly GameModeService _gameModeService;

	public bool DeathLinkEnabled { get; private set; }

	private bool _hasDied;
	private bool _receivedDeathLinkQueued;
	private bool _deathLinkSent = false;

	public DeathLinkService(
		RetroarchMemoryService retroarchMemoryService,
		GameModeService gameModeService,
		CurrentSceneService currentSceneService
	)
	{
		_retroarchMemoryService = retroarchMemoryService;
		_gameModeService = gameModeService;
		_currentSceneService = currentSceneService;
	}

	public async Task StoreDeathLinkEnabledFromMemory()
	{
		const uint deathLinkEnabledFlagAddress = 0xA040002B;

		var deathLinkEnabledFlag = await _retroarchMemoryService.Read8(deathLinkEnabledFlagAddress);

		DeathLinkEnabled = deathLinkEnabledFlag > 0;
	}

	public async Task ProcessDeathLink()
	{
		if (!DeathLinkEnabled)
		{
			return;
		}

		var currentGameMode = await _gameModeService.GetCurrentGameMode();

		if (!_hasDied && currentGameMode.Name == "Dying")
		{
			_hasDied = true;
		}

		if (_hasDied && currentGameMode.Name == "Normal Gameplay")
		{
			_receivedDeathLinkQueued = false;
			_deathLinkSent = false;
			_hasDied = false;
		}

		if (_receivedDeathLinkQueued && !_hasDied && currentGameMode.Name == "Normal Gameplay")
		{
			var currentScene = await _currentSceneService.GetCurrentScene();

			if (DeathCrashScenes.Contains(currentScene))
			{
				return;
			}

			const uint linkHealthAddress = 0xA011A600;
			await _retroarchMemoryService.Write16(address: linkHealthAddress, dataToWrite: 0);
		}
	}

	public void ReceiveDeathLink()
	{
		_receivedDeathLinkQueued = true;
	}

	// Call after ProcessDeathLinks
	public bool ShouldSendDeathLink()
	{
		if (_deathLinkSent)
		{
			return false;
		}

		if (!_receivedDeathLinkQueued && _hasDied)
		{
			_deathLinkSent = true;
			return true;
		}

		return false;
	}

	// The game crashes if killed in the market entrance or outside the ToT
	private static readonly HashSet<ushort> DeathCrashScenes = [27, 28, 29, 35, 36, 37];
}
