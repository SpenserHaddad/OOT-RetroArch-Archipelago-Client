namespace OOT_AP_Client;

public class LocationCheckService
{
	private readonly RetroarchMemoryService _retroarchMemoryService;

	public LocationCheckService(RetroarchMemoryService retroarchMemoryService)
	{
		_retroarchMemoryService = retroarchMemoryService;
	}

	// We're gonna want some sort of caching system for this so that it doesn't keep reporting every single location every single time
	// Will worry about that after this works though
	public async Task<List<string>> GetAllCheckedLocationNames()
	{
		var coopContext = await _retroarchMemoryService.ReadByteArray(address: 0xA040002c, numberOfBytes: 4);

		var checkedLocationNames = new List<string>();
		foreach (var locationInformation in AllLocationInformation.LocationInformations)
		{
			if (await CheckLocation(locationInformation: locationInformation, coopContext: coopContext))
			{
				checkedLocationNames.Add(locationInformation.Name);
			}
		}

		await _retroarchMemoryService.WriteByteArray(
			address: 0xA040002c,
			dataToWrite: [00, 00, 00, 00, 00, 00, 00, 00]
		);

		return checkedLocationNames;
	}

	private async Task<bool> CheckLocation(LocationInformation locationInformation, byte[] coopContext)
	{
		switch (locationInformation.Type)
		{
			case LocationType.Chest:
				return await CheckChestLocation(locationInformation: locationInformation, coopContext: coopContext);
			default:
				throw new InvalidOperationException(
					$"Unknown LocationType {locationInformation.Type} for location {locationInformation.Name}"
				);
		}
	}

	private async Task<bool> CheckChestLocation(LocationInformation locationInformation, byte[] coopContext)
	{
		return CoopContextCheck(
				coopContext: coopContext,
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
		const uint sceneFlagsOffset = 0xA011A6A4;

		var localSceneOffset = (uint)(sceneFlagsOffset + 0x1c * sceneOffset + sceneDataOffset);

		var nearbyMemory = await _retroarchMemoryService.Read32(localSceneOffset);

		return ((nearbyMemory >> bitToCheck) & 1) == 1;
	}

	private bool CoopContextCheck(byte[] coopContext, byte sceneOffset, byte bitToCheck, byte ootrLocationType)
	{
		return coopContext[0] == sceneOffset && coopContext[1] == ootrLocationType && coopContext[3] == bitToCheck;
	}
}
