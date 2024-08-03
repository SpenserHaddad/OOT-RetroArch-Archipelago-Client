namespace OOT_AP_Client;

public class LocationCheckService
{
	private readonly RetroarchMemoryService _retroarchMemoryService;

	public LocationCheckService(RetroarchMemoryService retroarchMemoryService)
	{
		_retroarchMemoryService = retroarchMemoryService;
	}

	// Going to want some sort of caching system for this so that it doesn't keep reporting every single location every single time
	// Might be as simple as a hashmap somewhere that gets loaded with all received items, and stuff only gets sent to the server when it's not in that hashmap
	public async Task<List<string>> GetAllCheckedLocationNames()
	{
		var outgoingItemKey = await _retroarchMemoryService.ReadByteArray(address: 0x8040002c, numberOfBytes: 4);

		// Since this is async with the emulator, there's a chance that the key
		// gets populated after we read it but before we write to clear it
		if (outgoingItemKey.Any((b) => b != 0x00))
		{
			await _retroarchMemoryService.WriteByteArray(
				address: 0x8040002c,
				dataToWrite: [00, 00, 00, 00]
			);
			await _retroarchMemoryService.WriteByteArray(
				address: 0x80400030,
				dataToWrite: [00, 00, 00, 00]
			);
		}

		var checkedLocationNames = new List<string>();
		foreach (var locationInformation in AllLocationInformation.LocationInformations)
		{
			if (await CheckLocation(locationInformation: locationInformation, outgoingItemKey: outgoingItemKey))
			{
				checkedLocationNames.Add(locationInformation.Name);
			}
		}

		return checkedLocationNames;
	}

	private async Task<bool> CheckLocation(LocationInformation locationInformation, byte[] outgoingItemKey)
	{
		return locationInformation.Type switch
		{
			LocationType.Chest => await CheckChestLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey
			),
			_ => throw new InvalidOperationException(
				$"Unknown LocationType {locationInformation.Type} for location {locationInformation.Name}"
			),
		};
	}

	private async Task<bool> CheckChestLocation(LocationInformation locationInformation, byte[] outgoingItemKey)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				ootrLocationType: 0x1
			) ||
			await SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0x0
			);
	}

	private async Task<bool> SceneCheck(byte sceneOffset, byte bitToCheck, byte sceneDataOffset)
	{
		const uint sceneFlagsOffset = 0x8011A6A4;

		var localSceneOffset = (uint)(sceneFlagsOffset + 0x1c * sceneOffset + sceneDataOffset);

		var nearbyMemory = await _retroarchMemoryService.Read32(localSceneOffset);

		return ((nearbyMemory >> bitToCheck) & 1) == 1;
	}

	private static bool OutgoingKeyCheck(
		byte[] outgoingItemKey,
		byte sceneOffset,
		byte bitToCheck,
		byte ootrLocationType
	)
	{
		return outgoingItemKey[0] == sceneOffset && outgoingItemKey[1] == ootrLocationType &&
			outgoingItemKey[3] == bitToCheck;
	}
}
