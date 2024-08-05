namespace OOT_AP_Client;

public class GameCompleteService
{
	private RetroarchMemoryService _retroarchMemoryService;

	private bool _isGameComplete = false;

	public GameCompleteService(RetroarchMemoryService retroarchMemoryService)
	{
		_retroarchMemoryService = retroarchMemoryService;
	}

	public async Task<bool> IsGameComplete()
	{
		if (_isGameComplete)
		{
			return true;
		}

		const uint scenePointerAddress = 0xA01CA208;
		var scenePointerValue = (uint)await _retroarchMemoryService.Read32(scenePointerAddress);

		const uint triforceHuntCompleteCreditsCutscenePointer = 0x80383C10;
		const uint ganonDefeatedCutscenePointer = 0x80382720;

		if (scenePointerValue is not (triforceHuntCompleteCreditsCutscenePointer or ganonDefeatedCutscenePointer))
		{
			return false;
		}

		_isGameComplete = true;
		return true;
	}
}
