using OOT_AP_Client.Services.Interfaces;

namespace OOT_AP_Client.OcarinaOfTime.Services;

public class CurrentSceneService
{
	private IMemoryService _memoryService;

	public CurrentSceneService(IMemoryService memoryService)
	{
		_memoryService = memoryService;
	}

	public async Task<ushort> GetCurrentScene()
	{
		const uint currentSceneAddress = 0xA01C8544;

		return (ushort)await _memoryService.Read16(currentSceneAddress);
	}
}
