using OOT_AP_Client.Models;
using OOT_AP_Client.OcarinaOfTime.Models;
using OOT_AP_Client.Services.Interfaces;
using OOT_AP_Client.Utils;

namespace OOT_AP_Client.OcarinaOfTime.Services;

public class CollectibleCheckService
{
	private readonly IMemoryService _memoryService;

	public CollectibleCheckService(IMemoryService memoryService)
	{
		_memoryService = memoryService;
	}

	public async Task<List<long>> GetAllCheckedCollectibleIds(
		long collectibleOverridesFlagAddress,
		List<CollectibleFlagOffset> collectibleFlagOffsets
	)
	{
		var checkedCollectibleIds = new List<long>();

		var memoryReadCommands = new List<MemoryReadCommand>();
		var alreadyQueuedOffsets = new HashSet<long>();
		foreach (var collectibleFlagOffset in collectibleFlagOffsets)
		{
			var addressOfTargetByte = GetAddressForCollectibleOffset(
				collectibleOverridesFlagAddress: collectibleOverridesFlagAddress,
				collectibleFlagOffset: collectibleFlagOffset
			);

			if (alreadyQueuedOffsets.Contains(addressOfTargetByte))
			{
				continue;
			}

			var memoryReadCommand = new MemoryReadCommand
			{
				Address = addressOfTargetByte,
				NumberOfBytes = 1,
			};
			memoryReadCommands.Add(memoryReadCommand);

			alreadyQueuedOffsets.Add(addressOfTargetByte);
		}

		var memoryDictionary = await _memoryService.ReadMemoryToLongMulti(memoryReadCommands);

		foreach (var collectibleFlagOffset in collectibleFlagOffsets)
		{
			var addressOfTargetByte = GetAddressForCollectibleOffset(
				collectibleOverridesFlagAddress: collectibleOverridesFlagAddress,
				collectibleFlagOffset: collectibleFlagOffset
			);
			var memoryContainingFlag = memoryDictionary[addressOfTargetByte];

			if (ByteUtils.CheckBit(
					memoryToCheck: memoryContainingFlag,
					bitToCheck: (byte)(collectibleFlagOffset.Flag % 8)
				))
			{
				checkedCollectibleIds.Add(collectibleFlagOffset.ItemId);
			}
		}

		return checkedCollectibleIds;
	}

	private long GetAddressForCollectibleOffset(
		long collectibleOverridesFlagAddress,
		CollectibleFlagOffset collectibleFlagOffset
	)
	{
		// eg 0 to 7 gives 0, 8 to 15 gives 1, index to which byte contains the bit we want
		var byteContainingTargetBit = collectibleFlagOffset.Flag >> 3;
		var addressOfTargetByte = collectibleOverridesFlagAddress
			+ collectibleFlagOffset.Offset
			+ byteContainingTargetBit;

		return addressOfTargetByte;
	}
}
