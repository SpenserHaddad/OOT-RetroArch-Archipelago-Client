using OOT_AP_Client.Services.Interfaces;

namespace OOT_AP_Client.OcarinaOfTime.Services;

public class OOTClientDeathLinkService
{
	private readonly IMemoryService _memoryService;
	private readonly CurrentSceneService _currentSceneService;
	private readonly GameModeService _gameModeService;

	public bool DeathLinkEnabled { get; private set; }

	private bool _hasDied;
	private bool _receivedDeathLinkQueued;
	private bool _deathLinkSent = false;

	public OOTClientDeathLinkService(
		IMemoryService memoryService,
		GameModeService gameModeService,
		CurrentSceneService currentSceneService
	)
	{
		_memoryService = memoryService;
		_gameModeService = gameModeService;
		_currentSceneService = currentSceneService;
	}

	public async Task StoreDeathLinkEnabledFromMemory()
	{
		const uint deathLinkEnabledFlagAddress = 0xA040002B;

		var deathLinkEnabledFlag = await _memoryService.Read8(deathLinkEnabledFlagAddress);

		DeathLinkEnabled = deathLinkEnabledFlag > 0;
	}

	/// <summary>
	/// Returns immediately if death link is not enabled.
	/// If death link is enabled, handles the logic for killing Link if a death link is queued,
	/// and sending out death link when link dies.
	/// </summary>
	/// <returns>Bool of whether to send a death link out</returns>
	public async Task<bool> ProcessDeathLink()
	{
		if (!DeathLinkEnabled)
		{
			return false;
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
				return false;
			}

			const uint linkHealthAddress = 0xA011A600;
			await _memoryService.Write16(address: linkHealthAddress, dataToWrite: 0);
		}

		return ShouldSendDeathLink();
	}

	public void ReceiveDeathLink()
	{
		_receivedDeathLinkQueued = true;
	}

	private bool ShouldSendDeathLink()
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
