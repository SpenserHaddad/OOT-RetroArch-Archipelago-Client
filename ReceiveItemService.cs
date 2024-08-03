using Archipelago.MultiClient.Net.Models;

namespace OOT_AP_Client;

public class ReceiveItemService
{
	private readonly RetroarchMemoryService _retroarchMemoryService;

	public ReceiveItemService(RetroarchMemoryService retroarchMemoryService)
	{
		_retroarchMemoryService = retroarchMemoryService;
	}

	public async Task<short> GetLocalReceivedItemIndex()
	{
		const uint localReceivedItemsCountAddress = 0xA011A660;
		var localReceivedItemsCount = await _retroarchMemoryService.Read16(localReceivedItemsCountAddress);

		return localReceivedItemsCount;
	}

	public async Task ReceiveItem(ItemInfo item)
	{
		const uint incomingPlayerAddress = 0xA0400026;
		const uint incomingItemAddress = 0xA0400028;

		await _retroarchMemoryService.Write16(address: incomingPlayerAddress, dataToWrite: 0x00);
		await _retroarchMemoryService.Write16(address: incomingItemAddress, dataToWrite: (short)(item.ItemId - 66000));
	}
}
