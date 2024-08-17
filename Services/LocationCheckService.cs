using OOT_AP_Client.Data;
using OOT_AP_Client.Enums;
using OOT_AP_Client.Models;
using OOT_AP_Client.Utils;

namespace OOT_AP_Client.Services;

public class LocationCheckService
{
	private readonly RetroarchMemoryService _retroarchMemoryService;
	private readonly GameModeService _gameModeService;

	private HashSet<Area> AreasToSkipChecking = [];

	public LocationCheckService(RetroarchMemoryService retroarchMemoryService, GameModeService gameModeService)
	{
		_retroarchMemoryService = retroarchMemoryService;
		_gameModeService = gameModeService;
	}

	public async Task InitializeMasterQuestHandling()
	{
		var masterQuestTableAddress
			= (uint)(0xA0400000 + (await _retroarchMemoryService.Read32(0xA0400E9F) - 0x03480000));

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

	// Going to want some sort of caching system for this so that it doesn't keep reporting every single location every single time
	// Might be as simple as a hashmap somewhere that gets loaded with all received items, and stuff only gets sent to the server when it's not in that hashmap
	public async Task<List<string>> GetAllCheckedLocationNames(SlotSettings slotSettings)
	{
		var outgoingItemKey
			= await _retroarchMemoryService.ReadMemoryToByteArray(address: 0x8040002c, numberOfBytes: 4);

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
		foreach (var locationInformation in AllLocationInformation.AllLocations)
		{
			if (AreasToSkipChecking.Contains(locationInformation.Area))
			{
				continue;
			}

			if (!await CheckLocation(
					locationInformation: locationInformation,
					outgoingItemKey: outgoingItemKey,
					slotSettings: slotSettings
				))
			{
				continue;
			}

			// make sure the game wasn't reset before marking this as checked
			var currentGameMode = await _gameModeService.GetCurrentGameMode();
			if (!currentGameMode.IsInGame)
			{
				return checkedLocationNames;
			}

			checkedLocationNames.Add(locationInformation.Name);
		}

		return checkedLocationNames;
	}

	private async Task<bool> CheckLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey,
		SlotSettings slotSettings
	)
	{
		return locationInformation.Type switch
		{
			LocationType.Chest => await CheckChestLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey
			),
			LocationType.Cow => await CheckCowLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey
			),
			LocationType.Skulltula => await CheckSkulltulaLocation(locationInformation),
			LocationType.Shop => await CheckShopLocation(
				locationInformation: locationInformation
			),
			LocationType.GroundItem => await CheckGroundItemLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey
			),
			LocationType.Event => await CheckEventLocation(locationInformation),
			LocationType.GetInfo => await CheckGetInfoLocation(locationInformation),
			LocationType.InfoTable => await CheckInfoTableLocation(locationInformation),
			LocationType.Scrubsanity => await CheckScrubsanityLocation(
				locationInformation: locationInformation,
				slotSettings: slotSettings
			),
			LocationType.BossItem => await CheckBossItemLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey
			),
			LocationType.BigPoeBottle => await CheckBigPoeBottleLocation(),
			LocationType.GreatFairy => await CheckGreatFairyMagicLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey
			),
			LocationType.TrailGreatFairy => await CheckTrailGreatFairyMagicLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey
			),
			LocationType.CraterGreatFairy => await CheckCraterGreatFairyMagicLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey
			),
			LocationType.Medigoron => await CheckMedigoronLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey
			),
			LocationType.BiggoronSword => await CheckBiggoronSwordLocation(),
			LocationType.BeanSale => await CheckBeanSaleLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey
			),
			LocationType.FishingChild => await CheckFishingLocation(false),
			LocationType.FishingAdult => await CheckFishingLocation(true),
			LocationType.FireArrows => await CheckFireArrowsLocation(
				locationInformation: locationInformation,
				outgoingItemKey: outgoingItemKey
			),
			LocationType.MembershipCardCheck => await CheckMembershipCardLocation(),
			LocationType.BombchuSalesman => await CheckBombchuMerchantLocation(
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

	private async Task<bool> CheckGroundItemLocation(LocationInformation locationInformation, byte[] outgoingItemKey)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				ootrLocationType: 0x2
			) ||
			await SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0xC
			);
	}

	private async Task<bool> CheckBossItemLocation(LocationInformation locationInformation, byte[] outgoingItemKey)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: 0x4F,
				ootrLocationType: 0x0
			) ||
			await SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: 0x1F,
				sceneDataOffset: 0xC
			);
	}

	// updates save context immediately, so the existing client doesn't check temp context
	// need to figure out the temp context format eventually so that we can do temp context more efficiently later
	private async Task<bool> CheckScrubsanityLocation(
		LocationInformation locationInformation,
		SlotSettings slotSettings
	)
	{
		if (!slotSettings.ShuffleScrubs)
		{
			return false;
		}

		return await SceneCheck(
			sceneOffset: locationInformation.Offset,
			bitToCheck: locationInformation.BitToCheck,
			sceneDataOffset: 0x10
		);
	}

	private async Task<bool> CheckCowLocation(LocationInformation locationInformation, byte[] outgoingItemKey)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: (byte)(locationInformation.BitToCheck - 0x03),
				ootrLocationType: 0x0
			) ||
			await SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0xC
			);
	}

	private async Task<bool> CheckGreatFairyMagicLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				ootrLocationType: 0x0
			) ||
			await SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0x04
			);
	}

	private async Task<bool> CheckTrailGreatFairyMagicLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: 0xFF,
				bitToCheck: 0x13,
				ootrLocationType: 0x05
			) ||
			await SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0x04
			);
	}

	private async Task<bool> CheckCraterGreatFairyMagicLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: 0xFF,
				bitToCheck: 0x14,
				ootrLocationType: 0x05
			) ||
			await SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0x04
			);
	}

	private async Task<bool> CheckMedigoronLocation(LocationInformation locationInformation, byte[] outgoingItemKey)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: 0x16,
				ootrLocationType: 0x0
			) ||
			await SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0xC
			);
	}

	private async Task<bool> CheckBeanSaleLocation(LocationInformation locationInformation, byte[] outgoingItemKey)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: 0x16,
				ootrLocationType: 0x0
			) ||
			await SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0xC
			);
	}

	private async Task<bool> CheckBombchuMerchantLocation(
		LocationInformation locationInformation,
		byte[] outgoingItemKey
	)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: 0x03,
				ootrLocationType: 0x0
			) ||
			await SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0xC
			);
	}

	// TODO: need to document and figure this out more
	private async Task<bool> CheckSkulltulaLocation(
		LocationInformation locationInformation
	)
	{
		const uint skulltulaFlagsOffset = 0xA011B46C;

		var skulltulaArrayIndex = locationInformation.Offset + 3 - 2 * (locationInformation.Offset % 4);
		var localSkulltulaOffset = (uint)(skulltulaFlagsOffset + skulltulaArrayIndex);
		var nearbyMemory = await _retroarchMemoryService.Read8(localSkulltulaOffset);

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: locationInformation.BitToCheck);
	}

	private async Task<bool> CheckShopLocation(LocationInformation locationInformation)
	{
		const uint shopContextOffset = 0xA011AB84;

		var nearbyMemory = await _retroarchMemoryService.Read32(shopContextOffset);

		var bitToCheck = (byte)(locationInformation.Offset * 4 + locationInformation.BitToCheck);

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: bitToCheck);
	}

	// Checked via looking at the points on the card, rather than getting the item itself
	private async Task<bool> CheckBigPoeBottleLocation()
	{
		const uint bigPoesRequiredAddress = 0xA0400EAD;
		var bigPoesRequired = await _retroarchMemoryService.Read8(bigPoesRequiredAddress);
		var pointsRequired = bigPoesRequired * 10;

		const uint bigPoesPointsAddress = 0xA011B48C;
		var bigPoesPoints = await _retroarchMemoryService.Read32(bigPoesPointsAddress);

		return bigPoesPoints >= pointsRequired;
	}

	private async Task<bool> CheckEventLocation(LocationInformation locationInformation)
	{
		const uint eventContextAddress = 0xA011B4A4;

		var eventAddress = (uint)(eventContextAddress + locationInformation.Offset * 2);

		var eventRow = await _retroarchMemoryService.Read16(eventAddress);

		return ByteUtils.CheckBit(memoryToCheck: eventRow, bitToCheck: locationInformation.BitToCheck);
	}

	private async Task<bool> CheckGetInfoLocation(LocationInformation locationInformation)
	{
		const uint getInfoStartAddress = 0xA011B4C0;

		var itemGetInfoAddress = getInfoStartAddress + locationInformation.Offset;

		var nearbyMemory = await _retroarchMemoryService.Read8(itemGetInfoAddress);

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: locationInformation.BitToCheck);
	}

	private async Task<bool> CheckInfoTableLocation(LocationInformation locationInformation)
	{
		const uint infoTableStartAddress = 0xA011B4C8;

		var itemInfoTableAddress = infoTableStartAddress + locationInformation.Offset;

		var nearbyMemory = await _retroarchMemoryService.Read8(itemInfoTableAddress);

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: locationInformation.BitToCheck);
	}

	private async Task<bool> CheckMembershipCardLocation()
	{
		const uint eventContextAddress = 0xA011B4A4;
		var eventAddress = eventContextAddress + 0x9 * 2;

		var eventRow = await _retroarchMemoryService.Read16(eventAddress);

		return ByteUtils.CheckBit(memoryToCheck: eventRow, bitToCheck: 0)
			&& ByteUtils.CheckBit(memoryToCheck: eventRow, bitToCheck: 1)
			&& ByteUtils.CheckBit(memoryToCheck: eventRow, bitToCheck: 2)
			&& ByteUtils.CheckBit(memoryToCheck: eventRow, bitToCheck: 3);
	}

	private async Task<bool> CheckFishingLocation(bool isAdult)
	{
		const uint fishingContextAddress = 0xA011B490;

		var bitToCheck = (byte)(isAdult ? 11 : 10);

		var nearbyMemory = await _retroarchMemoryService.Read32(fishingContextAddress);

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: bitToCheck);
	}

	private async Task<bool> CheckFireArrowsLocation(LocationInformation locationInformation, byte[] outgoingItemKey)
	{
		return OutgoingKeyCheck(
				outgoingItemKey: outgoingItemKey,
				sceneOffset: locationInformation.Offset,
				bitToCheck: 0x58,
				ootrLocationType: 0x0
			) ||
			await SceneCheck(
				sceneOffset: locationInformation.Offset,
				bitToCheck: locationInformation.BitToCheck,
				sceneDataOffset: 0x0
			);
	}

	private async Task<bool> CheckBiggoronSwordLocation()
	{
		const uint equipmentOffset = 0xA011A640;

		var nearbyMemory = await _retroarchMemoryService.Read32(equipmentOffset);

		const byte bitToCheck = 8;

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: bitToCheck);
	}

	private async Task<bool> SceneCheck(byte sceneOffset, byte bitToCheck, byte sceneDataOffset)
	{
		const uint sceneFlagsOffset = 0x8011A6A4;

		var localSceneOffset = (uint)(sceneFlagsOffset + 0x1c * sceneOffset + sceneDataOffset);

		var nearbyMemory = await _retroarchMemoryService.Read32(localSceneOffset);

		return ByteUtils.CheckBit(memoryToCheck: nearbyMemory, bitToCheck: bitToCheck);
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
