using Archipelago.MultiClient.Net.Models;

namespace OOT_AP_Client.Services;

public class ReceiveItemService
{
	private readonly CurrentSceneService _currentSceneService;
	private readonly RetroarchMemoryService _retroarchMemoryService;

	public ReceiveItemService(RetroarchMemoryService retroarchMemoryService, CurrentSceneService currentSceneService)
	{
		_retroarchMemoryService = retroarchMemoryService;
		_currentSceneService = currentSceneService;
	}

	public async Task<short> GetLocalReceivedItemIndex()
	{
		const uint localReceivedItemsCountAddress = 0xA011A660;
		var localReceivedItemsCount = await _retroarchMemoryService.Read16(localReceivedItemsCountAddress);

		return localReceivedItemsCount;
	}

	public async Task ReceiveItem(ItemInfo item)
	{
		if (ShopScenes.Contains(await _currentSceneService.GetCurrentScene()))
		{
			return;
		}

		const uint incomingPlayerAddress = 0xA0400026;
		const uint incomingItemAddress = 0xA0400028;

		await _retroarchMemoryService.Write16(address: incomingPlayerAddress, dataToWrite: 0x00);
		await _retroarchMemoryService.Write16(address: incomingItemAddress, dataToWrite: (short)(item.ItemId - 66000));
	}

	private static readonly HashSet<ushort> ShopScenes = [0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33, 0x42, 0x4B];
}
