using OOT_AP_Client.Services.Interfaces;

namespace OOT_AP_Client.OcarinaOfTime.Services;

public class GameCompleteService
{
	private IMemoryService _memoryService;

	private bool _isGameComplete = false;

	public GameCompleteService(IMemoryService memoryService)
	{
		_memoryService = memoryService;
	}

	public async Task<bool> IsGameComplete()
	{
		if (_isGameComplete)
		{
			return true;
		}

		const uint scenePointerAddress = 0xA01CA208;
		var scenePointerValue = (uint)await _memoryService.Read32(scenePointerAddress);

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
