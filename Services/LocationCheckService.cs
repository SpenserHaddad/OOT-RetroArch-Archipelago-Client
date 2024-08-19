using System.Collections.Immutable;
using OOT_AP_Client.Data;
using OOT_AP_Client.Enums;
using OOT_AP_Client.Models;
using OOT_AP_Client.Utils;

namespace OOT_AP_Client.Services;

public class LocationCheckService
{
	private readonly GameModeService _gameModeService;
	private readonly RetroarchMemoryService _retroarchMemoryService;

	private readonly HashSet<Area> AreasToSkipChecking = [];

	private int bigPoePointsRequired = int.MaxValue;

	public LocationCheckService(RetroarchMemoryService retroarchMemoryService, GameModeService gameModeService)
	{
		_retroarchMemoryService = retroarchMemoryService;
		_gameModeService = gameModeService;
	}

	public async Task InitializeMasterQuestHandling()
	{
		var masterQuestTableAddress
			= 0xA0400000 + (await _retroarchMemoryService.Read32(0xA0400E9F) - 0x03480000);

		var dungeonToDungeonId = new Dictionary<Area, byte>
		{
			{ Area.DekuTree, 0x0 },
			{ Area.DodongosCavern, 0x1 },
			{ Area.JabuJabusBelly, 0x2 },
			{ Area.ForestTemple, 0x3 },
			{ Area.FireTemple, 0x4 },
			{ Area.WaterTemple, 0x5 },
			{ Area.SpiritTemple, 0x6 },
			{ Area.ShadowTemple, 0x7 },
			{ Area.BottomOfTheWell, 0x8 },
			{ Area.IceCavern, 0x9 },
			{ Area.GerudoTrainingGround, 0xB },
			{ Area.GanonsCastle, 0xD },
		};

		foreach (var (area, dungeonId) in dungeonToDungeonId)
		{
			// Currently this takes advantage of the fact that the MQ versions of each dungeon are right after the regular one in the enum
			// Don't really like this though, should change it at some point
			var isMasterQuest = await _retroarchMemoryService.Read8(masterQuestTableAddress + dungeonId) == 1;
			var areaToSkip = isMasterQuest ? area : area + 1;

			AreasToSkipChecking.Add(areaToSkip);
		}
	}

	public async Task InitializeBigPoesRequired()
	{
		const long bigPoesRequiredAddress = 0xA0400EAD;
		var bigPoesRequired = await _retroarchMemoryService.Read8(bigPoesRequiredAddress);
		bigPoePointsRequired = bigPoesRequired * 10;
	}

	// Going to want some sort of caching system for this so that it doesn't keep reporting every single location every single time
	// Might be as simple as a hashmap somewhere that gets loaded with all received items, and stuff only gets sent to the server when it's not in that hashmap
	public async Task<List<string>> GetAllCheckedLocationNames(SlotSettings slotSettings)
	{
		var outgoingItemKey
			= await _retroarchMemoryService.ReadMemoryToByteArray(address: 0x8040002c, numberOfBytes: 4);

		// Since this is async with the emulator, there's a chance that the key
		// gets populated after we read it but before we write to clear it
		// So we make sure we actually read some data before clearing it
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

		var locationsToCheck
			= AllLocationInformation
				.AllLocations
				.Where((location) => !AreasToSkipChecking.Contains(location.Area))
				.Where((location) => location.Type != LocationType.Scrubsanity || slotSettings.ShuffleScrubs)
				.ToImmutableArray();

		var checkedMemoryAddresses = new HashSet<long>();
		var memoryReadCommands = new List<MemoryReadCommand>();
		foreach (var locationInformation in locationsToCheck)
		{
			var memoryReadCommand = GetMemoryReadCommandForLocation(locationInformation);

			if (!checkedMemoryAddresses.Add(memoryReadCommand.Address))
			{
				continue;
			}

			memoryReadCommands.Add(memoryReadCommand);
		}

		var memoryDictionary = await _retroarchMemoryService.ReadMemoryToLongMulti(memoryReadCommands);

		// make sure the game wasn't reset before using the memory that was read
		var currentGameMode = await _gameModeService.GetCurrentGameMode();
		if (!currentGameMode.IsInGame)
		{
			return [];
		}

		// Iterate over the locationsToCheck again, using the dictionary for memory checks now
		var checkedLocationNames = new List<string>();
		foreach (var locationInformation in locationsToCheck)
		{
			// change this to create memory read commands, should have a hashset to skip making duplicate commands
			if (!CheckLocation(
					locationInformation: locationInformation,
					outgoingItemKey: outgoingItemKey,
					memoryDictionary: memoryDictionary
				))
			{
				continue;
			}

			checkedLocationNames.Add(locationInformation.Name);
		}

		return checkedLocationNames;
	}

	private static MemoryReadCommand GetMemoryReadCommandForLocation(LocationInformation locationInformation)
	{
		switch (locationInformation.Type)
		{
			case LocationType.Chest:
			case LocationType.FireArrows:
				return GetChestFlagsReadCommand(locationInformation);
			case LocationType.GroundItem:
			case LocationType.BossItem:
			case LocationType.Cow:
			case LocationType.Medigoron:
			case LocationType.BeanSale:
			case LocationType.BombchuSalesman:
				return GetCollectibleFlagsReadCommand(locationInformation);
			case LocationType.GreatFairy:
			case LocationType.TrailGreatFairy:
			case LocationType.CraterGreatFairy:
				return GetGreatFairyReadCommand(locationInformation);
			case LocationType.Scrubsanity:
				return GetScrubsanityFlagsReadCommand(locationInformation);
			case LocationType.Skulltula:
				return GetSkulltulaReadCommand(locationInformation);
			case LocationType.Shop:
				return GetShopReadCommand();
			case LocationType.Event:
				return GetEventReadCommand(locationInformation);
			case LocationType.FishingChild:
			case LocationType.FishingAdult:
				return GetFishingReadCommand();
			case LocationType.GetInfo:
				return GetGetInfoReadCommand(locationInformation);
			case LocationType.InfoTable:
				return GetInfoTableReadCommand(locationInformation);
			case LocationType.BiggoronSword:
				return GetBiggoronSwordReadCommand();
			case LocationType.MembershipCardCheck:
				return GetMembershipCardReadCommand();
			case LocationType.BigPoeBottle:
				return GetBigPoeReadCommand();
			default:
				throw new InvalidOperationException(
					$"Unknown LocationType {locationInformation.Type} for location {locationInformation.Name}"
				);
		}
	}

	// This is also where the flag for Fire Arrows is
	private static MemoryReadCommand GetChestFlagsReadCommand(LocationInformation locationInformation)
	{
		return GetLocalSceneMemoryReadCommand(sceneOffset: locationInformation.Offset, sceneDataOffset: 0x0);
	}

	// Applies to all three types of Great Fairy check
	private static MemoryReadCommand GetGreatFairyReadCommand(LocationInformation locationInformation)
	{
		return GetLocalSceneMemoryReadCommand(sceneOffset: locationInformation.Offset, sceneDataOffset: 0x4);
	}

	private static MemoryReadCommand GetCollectibleFlagsReadCommand(LocationInformation locationInformation)
	{
		return GetLocalSceneMemoryReadCommand(sceneOffset: locationInformation.Offset, sceneDataOffset: 0xC);
	}

	private static MemoryReadCommand GetScrubsanityFlagsReadCommand(LocationInformation locationInformation)
	{
		return GetLocalSceneMemoryReadCommand(sceneOffset: locationInformation.Offset, sceneDataOffset: 0x10);
	}

	private static MemoryReadCommand GetSkulltulaReadCommand(LocationInformation locationInformation)
	{
		var skulltulaLocationAddress = GetSkulltulaLocationAddress(locationInformation);

		return new MemoryReadCommand
		{
			Address = skulltulaLocationAddress,
			NumberOfBytes = 1,
		};
	}

	private static MemoryReadCommand GetShopReadCommand()
	{
		// TODO: Should centralize and de-duplicate these offset constants
		const long shopContextOffset = 0xA011AB84;

		return new MemoryReadCommand { Address = shopContextOffset, NumberOfBytes = 4 };
	}

	private static MemoryReadCommand GetBigPoeReadCommand()
	{
		const long bigPoesPointsAddress = 0xA011B48C;

		return new MemoryReadCommand { Address = bigPoesPointsAddress, NumberOfBytes = 4 };
	}

	private static MemoryReadCommand GetEventReadCommand(LocationInformation locationInformation)
	{
		const long eventContextAddress = 0xA011B4A4;

		var eventAddress = eventContextAddress + locationInformation.Offset * 2;

		return new MemoryReadCommand { Address = eventAddress, NumberOfBytes = 2 };
	}

	private static MemoryReadCommand GetGetInfoReadCommand(LocationInformation locationInformation)
	{
		const long getInfoStartAddress = 0xA011B4C0;

		var itemGetInfoAddress = getInfoStartAddress + locationInformation.Offset;

		return new MemoryReadCommand { Address = itemGetInfoAddress, NumberOfBytes = 1 };
	}

	private static MemoryReadCommand GetInfoTableReadCommand(LocationInformation locationInformation)
	{
		const long infoTableStartAddress = 0xA011B4C8;

		var itemInfoTableAddress = infoTableStartAddress + locationInformation.Offset;

		return new MemoryReadCommand { Address = itemInfoTableAddress, NumberOfBytes = 1 };
	}

	private static MemoryReadCommand GetMembershipCardReadCommand()
	{
		const long eventContextAddress = 0xA011B4A4;
		const long eventAddress = eventContextAddress + 0x9 * 2;

		return new MemoryReadCommand { Address = eventAddress, NumberOfBytes = 2 };
	}

	private static MemoryReadCommand GetFishingReadCommand()
	{
		const long fishingContextAddress = 0xA011B490;

		return new MemoryReadCommand { Address = fishingContextAddress, NumberOfBytes = 4 };
	}

	private static MemoryReadCommand GetBiggoronSwordReadCommand()
	{
		const long equipmentOffset = 0xA011A640;

		return new MemoryReadCommand { Address = equipmentOffset, NumberOfBytes = 4 };
	}

	private bool CheckLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey,
		Dictionary<long, long> memoryDictionary
	)
	{
		return locationInformation.Type switch
		{
			LocationType.Chest => CheckChestLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey,
				memoryDictionary: memoryDictionary
			),
			LocationType.Cow => CheckCowLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey,
				memoryDictionary: memoryDictionary
			),
			LocationType.Skulltula => CheckSkulltulaLocation(
				locationInformation: locationInformation,
				memoryDictionary: memoryDictionary
			),
			LocationType.Shop => CheckShopLocation(
				locationInformation: locationInformation,
				memoryDictionary: memoryDictionary
			),
			LocationType.GroundItem => CheckGroundItemLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey,
				memoryDictionary: memoryDictionary
			),
			LocationType.Event => CheckEventLocation(
				locationInformation: locationInformation,
				memoryDictionary: memoryDictionary
			),
			LocationType.GetInfo => CheckGetInfoLocation(
				locationInformation: locationInformation,
				memoryDictionary: memoryDictionary
			),
			LocationType.InfoTable => CheckInfoTableLocation(
				locationInformation: locationInformation,
				memoryDictionary: memoryDictionary
			),
			LocationType.Scrubsanity => CheckScrubsanityLocation(
				locationInformation: locationInformation,
				memoryDictionary: memoryDictionary
			),
			LocationType.BossItem => CheckBossItemLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey,
				memoryDictionary: memoryDictionary
			),
			LocationType.BigPoeBottle => CheckBigPoeBottleLocation(
				memoryDictionary: memoryDictionary
			),
			LocationType.GreatFairy => CheckGreatFairyMagicLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey,
				memoryDictionary: memoryDictionary
			),
			LocationType.TrailGreatFairy => CheckTrailGreatFairyMagicLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey,
				memoryDictionary: memoryDictionary
			),
			LocationType.CraterGreatFairy => CheckCraterGreatFairyMagicLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey,
				memoryDictionary: memoryDictionary
			),
			LocationType.Medigoron => CheckMedigoronLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey,
				memoryDictionary: memoryDictionary
			),
			LocationType.BiggoronSword => CheckBiggoronSwordLocation(
				memoryDictionary: memoryDictionary
			),
			LocationType.BeanSale => CheckBeanSaleLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey,
				memoryDictionary: memoryDictionary
			),
			LocationType.FishingChild => CheckFishingLocation(
				isAdult: false,
				memoryDictionary: memoryDictionary
			),
			LocationType.FishingAdult => CheckFishingLocation(
				isAdult: true,
				memoryDictionary: memoryDictionary
			),
			LocationType.FireArrows => CheckFireArrowsLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey,
				memoryDictionary: memoryDictionary
			),
			LocationType.MembershipCardCheck => CheckMembershipCardLocation(
				memoryDictionary: memoryDictionary
			),
			LocationType.BombchuSalesman => CheckBombchuSalesmanLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey,
				memoryDictionary: memoryDictionary
			),
			_ => throw new InvalidOperationException(
				$"Unknown LocationType {locationInformation.Type} for location {locationInformation.Name}"
			),
		};
	}

	private bool CheckChestLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey,
		Dictionary<long, long> memoryDictionary
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				ootrLocationType: 0x1
			) ||
			SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0x0,
				memoryDictionary: memoryDictionary
			);
	}

	private bool CheckGroundItemLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey,
		Dictionary<long, long> memoryDictionary
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				ootrLocationType: 0x2
			) ||
			SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0xC,
				memoryDictionary: memoryDictionary
			);
	}

	private bool CheckBossItemLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey,
		Dictionary<long, long> memoryDictionary
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: 0x4F,
				ootrLocationType: 0x0
			) ||
			SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: 0x1F,
				sceneDataOffset: 0xC,
				memoryDictionary: memoryDictionary
			);
	}

	// updates save context immediately, so the existing client doesn't check temp context
	// need to figure out the temp context format eventually so that we can do temp context more efficiently later
	private bool CheckScrubsanityLocation(
		LocationInformation locationInformation,
		Dictionary<long, long> memoryDictionary
	)
	{
		return SceneCheck(
			sceneOffset: locationInformation.Offset,
			bitToCheck: locationInformation.BitToCheck,
			sceneDataOffset: 0x10,
			memoryDictionary: memoryDictionary
		);
	}

	private bool CheckCowLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey,
		Dictionary<long, long> memoryDictionary
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: (byte)(locationInformation.BitToCheck - 0x03),
				ootrLocationType: 0x0
			) ||
			SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0xC,
				memoryDictionary: memoryDictionary
			);
	}

	private bool CheckGreatFairyMagicLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey,
		Dictionary<long, long> memoryDictionary
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				ootrLocationType: 0x0
			) ||
			SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0x04,
				memoryDictionary: memoryDictionary
			);
	}

	private bool CheckTrailGreatFairyMagicLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey,
		Dictionary<long, long> memoryDictionary
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: 0xFF,
				bitToCheck: 0x13,
				ootrLocationType: 0x05
			) ||
			SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0x04,
				memoryDictionary: memoryDictionary
			);
	}

	private bool CheckCraterGreatFairyMagicLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey,
		Dictionary<long, long> memoryDictionary
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: 0xFF,
				bitToCheck: 0x14,
				ootrLocationType: 0x05
			) ||
			SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0x04,
				memoryDictionary: memoryDictionary
			);
	}

	private bool CheckMedigoronLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey,
		Dictionary<long, long> memoryDictionary
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: 0x16,
				ootrLocationType: 0x0
			) ||
			SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0xC,
				memoryDictionary: memoryDictionary
			);
	}

	private bool CheckBeanSaleLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey,
		Dictionary<long, long> memoryDictionary
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: 0x16,
				ootrLocationType: 0x0
			) ||
			SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0xC,
				memoryDictionary: memoryDictionary
			);
	}

	private bool CheckBombchuSalesmanLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey,
		Dictionary<long, long> memoryDictionary
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: 0x03,
				ootrLocationType: 0x0
			) ||
			SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0xC,
				memoryDictionary: memoryDictionary
			);
	}

	private static bool CheckSkulltulaLocation(
		LocationInformation locationInformation,
		Dictionary<long, long> memoryDictionary
	)
	{
		var skulltulaLocationAddress = GetSkulltulaLocationAddress(locationInformation);

		var nearbyMemory = memoryDictionary[skulltulaLocationAddress];

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: locationInformation.BitToCheck);
	}

	// TODO: need to document and figure this out more
	private static long GetSkulltulaLocationAddress(LocationInformation locationInformation)
	{
		const long skulltulaFlagsOffset = 0xA011B46C;

		var skulltulaArrayIndex = locationInformation.Offset + 3 - 2 * (locationInformation.Offset % 4);
		var localSkulltulaOffset = skulltulaFlagsOffset + skulltulaArrayIndex;

		return localSkulltulaOffset;
	}

	private bool CheckShopLocation(
		LocationInformation locationInformation,
		Dictionary<long, long> memoryDictionary
	)
	{
		const long shopContextOffset = 0xA011AB84;

		var nearbyMemory = memoryDictionary[shopContextOffset];

		var bitToCheck = locationInformation.Offset * 4 + locationInformation.BitToCheck;

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: (byte)bitToCheck);
	}

	// Checked via looking at the points on the card, rather than getting the item itself
	private bool CheckBigPoeBottleLocation(Dictionary<long, long> memoryDictionary)
	{
		const long bigPoesPointsAddress = 0xA011B48C;
		var bigPoesPoints = memoryDictionary[bigPoesPointsAddress];

		return bigPoesPoints >= bigPoePointsRequired;
	}

	private bool CheckEventLocation(
		LocationInformation locationInformation,
		Dictionary<long, long> memoryDictionary
	)
	{
		const long eventContextAddress = 0xA011B4A4;

		var eventAddress = eventContextAddress + locationInformation.Offset * 2;

		var eventRow = memoryDictionary[eventAddress];

		return ByteUtils.CheckBit(memoryToCheck: eventRow, bitToCheck: locationInformation.BitToCheck);
	}

	private bool CheckGetInfoLocation(
		LocationInformation locationInformation,
		Dictionary<long, long> memoryDictionary
	)
	{
		const long getInfoStartAddress = 0xA011B4C0;

		var itemGetInfoAddress = getInfoStartAddress + locationInformation.Offset;

		var nearbyMemory = memoryDictionary[itemGetInfoAddress];

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: locationInformation.BitToCheck);
	}

	private bool CheckInfoTableLocation(
		LocationInformation locationInformation,
		Dictionary<long, long> memoryDictionary
	)
	{
		const long infoTableStartAddress = 0xA011B4C8;

		var itemInfoTableAddress = infoTableStartAddress + locationInformation.Offset;

		var nearbyMemory = memoryDictionary[itemInfoTableAddress];

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: locationInformation.BitToCheck);
	}

	private bool CheckMembershipCardLocation(Dictionary<long, long> memoryDictionary)
	{
		const long eventContextAddress = 0xA011B4A4;
		const long eventAddress = eventContextAddress + 0x9 * 2;

		var eventRow = memoryDictionary[eventAddress];

		return ByteUtils.CheckBit(memoryToCheck: eventRow, bitToCheck: 0)
			&& ByteUtils.CheckBit(memoryToCheck: eventRow, bitToCheck: 1)
			&& ByteUtils.CheckBit(memoryToCheck: eventRow, bitToCheck: 2)
			&& ByteUtils.CheckBit(memoryToCheck: eventRow, bitToCheck: 3);
	}

	private bool CheckFishingLocation(bool isAdult, Dictionary<long, long> memoryDictionary)
	{
		const long fishingContextAddress = 0xA011B490;

		var bitToCheck = isAdult ? 11 : 10;

		var nearbyMemory = memoryDictionary[fishingContextAddress];

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: (byte)bitToCheck);
	}

	private bool CheckFireArrowsLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey,
		Dictionary<long, long> memoryDictionary
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: 0x58,
				ootrLocationType: 0x0
			) ||
			SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0x0,
				memoryDictionary: memoryDictionary
			);
	}

	private bool CheckBiggoronSwordLocation(Dictionary<long, long> memoryDictionary)
	{
		const long equipmentOffset = 0xA011A640;

		var nearbyMemory = memoryDictionary[equipmentOffset];

		const byte bitToCheck = 8;

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: bitToCheck);
	}

	private static bool SceneCheck(
		byte sceneOffset,
		byte bitToCheck,
		byte sceneDataOffset,
		Dictionary<long, long> memoryDictionary
	)
	{
		var localSceneOffset = GetLocalSceneOffset(sceneOffset: sceneOffset, sceneDataOffset: sceneDataOffset);

		var nearbyMemory = memoryDictionary[localSceneOffset];

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: bitToCheck);
	}

	private static MemoryReadCommand GetLocalSceneMemoryReadCommand(byte sceneOffset, byte sceneDataOffset)
	{
		var localSceneOffset = GetLocalSceneOffset(sceneOffset: sceneOffset, sceneDataOffset: sceneDataOffset);

		return new MemoryReadCommand
		{
			Address = localSceneOffset,
			NumberOfBytes = 4,
		};
	}

	private static long GetLocalSceneOffset(byte sceneOffset, byte sceneDataOffset)
	{
		const long sceneFlagsOffset = 0xA011A6A4;

		var localSceneOffset = sceneFlagsOffset + 0x1c * sceneOffset + sceneDataOffset;

		return localSceneOffset;
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
