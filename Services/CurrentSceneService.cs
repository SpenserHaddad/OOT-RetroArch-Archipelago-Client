namespace OOT_AP_Client.Services;

public class CurrentSceneService
{
	private RetroarchMemoryService _retroarchMemoryService;

	public CurrentSceneService(RetroarchMemoryService retroarchMemoryService)
	{
		_retroarchMemoryService = retroarchMemoryService;
	}

	public async Task<ushort> GetCurrentScene()
	{
		const uint currentSceneAddress = 0xA01C8544;

		return (ushort)await _retroarchMemoryService.Read16(currentSceneAddress);
	}
}
