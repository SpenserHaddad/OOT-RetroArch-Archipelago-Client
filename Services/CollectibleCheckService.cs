using OOT_AP_Client.Models;
using OOT_AP_Client.Utils;

namespace OOT_AP_Client.Services;

// Simple optimization for the initial release: have a structure that caches memory, maybe just a dictionary that gets emptied out on each new call to this function
// That way multiple calls for the same byte won't result in multiple separate fetches from retroarch
// To be even better, since lots of the accesses are close together, read as 4 byte chunks rounding down to the nearest multiple of 4
// Then put all 4 of those bytes in the cache
// In the future it would be nicer to just document the full range of memory to read for this and then read it with one command, but before that I'll need to fix reading more than 4 bytes at a time

public class CollectibleCheckService
{
	private readonly RetroarchMemoryService _retroarchMemoryService;

	public CollectibleCheckService(RetroarchMemoryService retroarchMemoryService)
	{
		_retroarchMemoryService = retroarchMemoryService;
	}

	public async Task<List<uint>> GetAllCheckedCollectibleIds(
		uint collectibleOverridesFlagAddress,
		List<CollectibleFlagOffset> collectibleFlagOffsets
	)
	{
		var memoryCache = new Dictionary<uint, byte>();

		var checkedCollectibleIds = new List<uint>();

		foreach (var collectibleFlagOffset in collectibleFlagOffsets)
		{
			// eg 0 to 7 gives 0, 8 to 15 gives 1, index to which byte contains the bit we want
			var byteContainingTargetBit = collectibleFlagOffset.Flag >> 3;
			var addressOfTargetByte = collectibleOverridesFlagAddress
				+ collectibleFlagOffset.Offset
				+ byteContainingTargetBit;

			var foundInCache = memoryCache.TryGetValue(addressOfTargetByte, out var memoryContainingFlag);

			if (!foundInCache)
			{
				memoryContainingFlag = await _retroarchMemoryService.Read8(addressOfTargetByte);
				memoryCache.Add(key: addressOfTargetByte, value: memoryContainingFlag);
			}

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
}
