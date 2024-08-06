using OOT_AP_Client.Enums;
using OOT_AP_Client.Models;

namespace OOT_AP_Client.Data;

public static class AllLocationInformation
{
	public static LocationInformation[] AllLocations { get; } =
	[
		// Kokiri Forest
		new LocationInformation(
			name: "KF Midos Top Left Chest",
			type: LocationType.Chest,
			offset: 0x28,
			bitToCheck: 0x00,
			area: Area.KokiriForest
		),
		new LocationInformation(
			name: "KF Midos Top Right Chest",
			type: LocationType.Chest,
			offset: 0x28,
			bitToCheck: 0x01,
			area: Area.KokiriForest
		),
		new LocationInformation(
			name: "KF Midos Bottom Left Chest",
			type: LocationType.Chest,
			offset: 0x28,
			bitToCheck: 0x02,
			area: Area.KokiriForest
		),
		new LocationInformation(
			name: "KF Midos Bottom Right Chest",
			type: LocationType.Chest,
			offset: 0x28,
			bitToCheck: 0x03,
			area: Area.KokiriForest
		),
		new LocationInformation(
			name: "KF Kokiri Sword Chest",
			type: LocationType.Chest,
			offset: 0x55,
			bitToCheck: 0x00,
			area: Area.KokiriForest
		),
		new LocationInformation(
			name: "KF Storms Grotto Chest",
			type: LocationType.Chest,
			offset: 0x3E,
			bitToCheck: 0x0C,
			area: Area.KokiriForest
		),

		new LocationInformation(
			name: "KF Links House Cow",
			type: LocationType.Cow,
			offset: 0x34,
			bitToCheck: 0x18,
			area: Area.KokiriForest
		),

		new LocationInformation(
			name: "KF GS Know It All House",
			type: LocationType.Skulltula,
			offset: 0x0C,
			bitToCheck: 0x01,
			area: Area.KokiriForest
		),
		new LocationInformation(
			name: "KF GS Bean Patch",
			type: LocationType.Skulltula,
			offset: 0x0C,
			bitToCheck: 0x00,
			area: Area.KokiriForest
		),
		new LocationInformation(
			name: "KF GS House of Twins",
			type: LocationType.Skulltula,
			offset: 0x0C,
			bitToCheck: 0x02,
			area: Area.KokiriForest
		),

		new LocationInformation(
			name: "KF Shop Item 5",
			type: LocationType.Shop,
			offset: 0x06,
			bitToCheck: 0x00,
			area: Area.KokiriForest
		),
		new LocationInformation(
			name: "KF Shop Item 6",
			type: LocationType.Shop,
			offset: 0x06,
			bitToCheck: 0x01,
			area: Area.KokiriForest
		),
		new LocationInformation(
			name: "KF Shop Item 7",
			type: LocationType.Shop,
			offset: 0x06,
			bitToCheck: 0x02,
			area: Area.KokiriForest
		),
		new LocationInformation(
			name: "KF Shop Item 8",
			type: LocationType.Shop,
			offset: 0x06,
			bitToCheck: 0x03,
			area: Area.KokiriForest
		),

		new LocationInformation(
			name: "KF Shop Blue Rupee",
			type: LocationType.GroundItem,
			offset: 0x2D,
			bitToCheck: 0x01,
			area: Area.KokiriForest
		),

		// Lost Woods
		new LocationInformation(
			name: "LW Gift from Saria",
			type: LocationType.Event,
			offset: 0x0C,
			bitToCheck: 0x01,
			area: Area.LostWoods
		),
		new LocationInformation(
			name: "LW Ocarina Memory Game",
			type: LocationType.GetInfo,
			offset: 0x03,
			bitToCheck: 0x07,
			area: Area.LostWoods
		),
		new LocationInformation(
			name: "LW Target in Woods",
			type: LocationType.GetInfo,
			offset: 0x02,
			bitToCheck: 0x05,
			area: Area.LostWoods
		),
		new LocationInformation(
			name: "LW Near Shortcuts Grotto Chest",
			type: LocationType.Chest,
			offset: 0x3E,
			bitToCheck: 0x14,
			area: Area.LostWoods
		),
		new LocationInformation(
			name: "Deku Theater Skull Mask",
			type: LocationType.GetInfo,
			offset: 0x02,
			bitToCheck: 0x06,
			area: Area.LostWoods
		),
		new LocationInformation(
			name: "Deku Theater Mask of Truth",
			type: LocationType.GetInfo,
			offset: 0x02,
			bitToCheck: 0x07,
			area: Area.LostWoods
		),
		new LocationInformation(
			name: "LW Skull Kid",
			type: LocationType.GetInfo,
			offset: 0x03,
			bitToCheck: 0x06,
			area: Area.LostWoods
		),

		new LocationInformation(
			name: "LW Deku Scrub Near Bridge",
			type: LocationType.InfoTable,
			offset: 0x33,
			bitToCheck: 0x02,
			area: Area.LostWoods
		),
		new LocationInformation(
			name: "LW Deku Scrub Near Bridge",
			type: LocationType.Scrubsanity,
			offset: 0x5B,
			bitToCheck: 0x0A,
			area: Area.LostWoods
		),
		new LocationInformation(
			name: "LW Deku Scrub Grotto Front",
			type: LocationType.InfoTable,
			offset: 0x33,
			bitToCheck: 0x03,
			area: Area.LostWoods
		),
		new LocationInformation(
			name: "LW Deku Scrub Grotto Front",
			type: LocationType.Scrubsanity,
			offset: 0x1F,
			bitToCheck: 0x0B,
			area: Area.LostWoods
		),

		new LocationInformation(
			name: "LW Deku Scrub Near Deku Theater Left",
			type: LocationType.Scrubsanity,
			offset: 0x5B,
			bitToCheck: 0x02,
			area: Area.LostWoods
		),
		new LocationInformation(
			name: "LW Deku Scrub Near Deku Theater Right",
			type: LocationType.Scrubsanity,
			offset: 0x5B,
			bitToCheck: 0x01,
			area: Area.LostWoods
		),
		new LocationInformation(
			name: "LW Deku Scrub Grotto Rear",
			type: LocationType.Scrubsanity,
			offset: 0x1F,
			bitToCheck: 0x04,
			area: Area.LostWoods
		),

		new LocationInformation(
			name: "LW GS Bean Patch Near Bridge",
			type: LocationType.Skulltula,
			offset: 0x0D,
			bitToCheck: 0x00,
			area: Area.LostWoods
		),
		new LocationInformation(
			name: "LW GS Bean Patch Near Theater",
			type: LocationType.Skulltula,
			offset: 0x0D,
			bitToCheck: 0x01,
			area: Area.LostWoods
		),
		new LocationInformation(
			name: "LW GS Above Theater",
			type: LocationType.Skulltula,
			offset: 0x0D,
			bitToCheck: 0x02,
			area: Area.LostWoods
		),

		// Sacred Forest Meadow
		new LocationInformation(
			name: "SFM Wolfos Grotto Chest",
			type: LocationType.Chest,
			offset: 0x3E,
			bitToCheck: 0x11,
			area: Area.SacredForestMeadow
		),
		new LocationInformation(
			name: "SFM Deku Scrub Grotto Front",
			type: LocationType.Scrubsanity,
			offset: 0x18,
			bitToCheck: 0x09,
			area: Area.SacredForestMeadow
		),
		new LocationInformation(
			name: "SFM Deku Scrub Grotto Rear",
			type: LocationType.Scrubsanity,
			offset: 0x18,
			bitToCheck: 0x08,
			area: Area.SacredForestMeadow
		),
		new LocationInformation(
			name: "SFM GS",
			type: LocationType.Skulltula,
			offset: 0x0D,
			bitToCheck: 0x03,
			area: Area.SacredForestMeadow
		),
		new LocationInformation(
			name: "Song from Saria",
			type: LocationType.Event,
			offset: 0x05,
			bitToCheck: 0x07,
			area: Area.SacredForestMeadow
		),
		new LocationInformation(
			name: "Sheik in Forest",
			type: LocationType.Event,
			offset: 0x05,
			bitToCheck: 0x00,
			area: Area.SacredForestMeadow
		),

		// Deku Tree
		new LocationInformation(
			name: "Deku Tree Map Chest",
			type: LocationType.Chest,
			offset: 0x00,
			bitToCheck: 0x03,
			area: Area.DekuTree
		),
		new LocationInformation(
			name: "Deku Tree Slingshot Room Side Chest",
			type: LocationType.Chest,
			offset: 0x00,
			bitToCheck: 0x05,
			area: Area.DekuTree
		),
		new LocationInformation(
			name: "Deku Tree Slingshot Chest",
			type: LocationType.Chest,
			offset: 0x00,
			bitToCheck: 0x01,
			area: Area.DekuTree
		),
		new LocationInformation(
			name: "Deku Tree Compass Chest",
			type: LocationType.Chest,
			offset: 0x00,
			bitToCheck: 0x02,
			area: Area.DekuTree
		),
		new LocationInformation(
			name: "Deku Tree Compass Room Side Chest",
			type: LocationType.Chest,
			offset: 0x00,
			bitToCheck: 0x06,
			area: Area.DekuTree
		),
		new LocationInformation(
			name: "Deku Tree Basement Chest",
			type: LocationType.Chest,
			offset: 0x00,
			bitToCheck: 0x04,
			area: Area.DekuTree
		),

		new LocationInformation(
			name: "Deku Tree GS Compass Room",
			type: LocationType.Skulltula,
			offset: 0x00,
			bitToCheck: 0x03,
			area: Area.DekuTree
		),
		new LocationInformation(
			name: "Deku Tree GS Basement Vines",
			type: LocationType.Skulltula,
			offset: 0x00,
			bitToCheck: 0x02,
			area: Area.DekuTree
		),
		new LocationInformation(
			name: "Deku Tree GS Basement Gate",
			type: LocationType.Skulltula,
			offset: 0x00,
			bitToCheck: 0x01,
			area: Area.DekuTree
		),
		new LocationInformation(
			name: "Deku Tree GS Basement Back Room",
			type: LocationType.Skulltula,
			offset: 0x00,
			bitToCheck: 0x00,
			area: Area.DekuTree
		),

		new LocationInformation(
			name: "Deku Tree Queen Gohma Heart",
			type: LocationType.BossItem,
			offset: 0x11,
			bitToCheck: default,
			area: Area.DekuTree
		),

		// Deku Tree MQ
		new LocationInformation(
			name: "Deku Tree MQ Map Chest",
			type: LocationType.Chest,
			offset: 0x00,
			bitToCheck: 0x03,
			area: Area.DekuTreeMQ
		),
		new LocationInformation(
			name: "Deku Tree MQ Slingshot Chest",
			type: LocationType.Chest,
			offset: 0x00,
			bitToCheck: 0x06,
			area: Area.DekuTreeMQ
		),
		new LocationInformation(
			name: "Deku Tree MQ Slingshot Room Back Chest",
			type: LocationType.Chest,
			offset: 0x00,
			bitToCheck: 0x02,
			area: Area.DekuTreeMQ
		),
		new LocationInformation(
			name: "Deku Tree MQ Compass Chest",
			type: LocationType.Chest,
			offset: 0x00,
			bitToCheck: 0x01,
			area: Area.DekuTreeMQ
		),
		new LocationInformation(
			name: "Deku Tree MQ Basement Chest",
			type: LocationType.Chest,
			offset: 0x00,
			bitToCheck: 0x04,
			area: Area.DekuTreeMQ
		),
		new LocationInformation(
			name: "Deku Tree MQ Before Spinning Log Chest",
			type: LocationType.Chest,
			offset: 0x00,
			bitToCheck: 0x05,
			area: Area.DekuTreeMQ
		),
		new LocationInformation(
			name: "Deku Tree MQ After Spinning Log Chest",
			type: LocationType.Chest,
			offset: 0x00,
			bitToCheck: 0x00,
			area: Area.DekuTreeMQ
		),

		new LocationInformation(
			name: "Deku Tree MQ Deku Scrub",
			type: LocationType.Scrubsanity,
			offset: 0x00,
			bitToCheck: 0x05,
			area: Area.DekuTreeMQ
		),

		new LocationInformation(
			name: "Deku Tree MQ GS Lobby",
			type: LocationType.Skulltula,
			offset: 0x00,
			bitToCheck: 0x01,
			area: Area.DekuTreeMQ
		),
		new LocationInformation(
			name: "Deku Tree MQ GS Compass Room",
			type: LocationType.Skulltula,
			offset: 0x00,
			bitToCheck: 0x03,
			area: Area.DekuTreeMQ
		),
		new LocationInformation(
			name: "Deku Tree MQ GS Basement Graves Room",
			type: LocationType.Skulltula,
			offset: 0x00,
			bitToCheck: 0x02,
			area: Area.DekuTreeMQ
		),
		new LocationInformation(
			name: "Deku Tree MQ GS Basement Back Room",
			type: LocationType.Skulltula,
			offset: 0x00,
			bitToCheck: 0x00,
			area: Area.DekuTreeMQ
		),

		new LocationInformation(
			name: "Deku Tree MQ Queen Gohma Heart",
			type: LocationType.BossItem,
			offset: 0x11,
			bitToCheck: default,
			area: Area.DekuTreeMQ
		),

		// Forest Temple
		new LocationInformation(
			name: "Forest Temple First Room Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x03,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple First Stalfos Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x00,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple Raised Island Courtyard Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x05,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple Map Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x01,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple Well Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x09,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple Eye Switch Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x04,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple Boss Key Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x0E,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple Floormaster Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x02,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple Red Poe Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x0D,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple Bow Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x0C,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple Blue Poe Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x0F,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple Falling Ceiling Room Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x07,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple Basement Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x0B,
			area: Area.ForestTemple
		),

		new LocationInformation(
			name: "Forest Temple GS First Room",
			type: LocationType.Skulltula,
			offset: 0x03,
			bitToCheck: 0x01,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple GS Lobby",
			type: LocationType.Skulltula,
			offset: 0x03,
			bitToCheck: 0x03,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple GS Raised Island Courtyard",
			type: LocationType.Skulltula,
			offset: 0x03,
			bitToCheck: 0x00,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple GS Level Island Courtyard",
			type: LocationType.Skulltula,
			offset: 0x03,
			bitToCheck: 0x02,
			area: Area.ForestTemple
		),
		new LocationInformation(
			name: "Forest Temple GS Basement",
			type: LocationType.Skulltula,
			offset: 0x03,
			bitToCheck: 0x04,
			area: Area.ForestTemple
		),

		new LocationInformation(
			name: "Forest Temple Phantom Ganon Heart",
			type: LocationType.BossItem,
			offset: 0x14,
			bitToCheck: default,
			area: Area.ForestTemple
		),

		// Forest Temple MQ
		new LocationInformation(
			name: "Forest Temple MQ First Room Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x03,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ Wolfos Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x00,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ Well Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x09,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ Raised Island Courtyard Lower Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x01,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ Raised Island Courtyard Upper Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x05,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ Boss Key Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x0E,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ Redead Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x02,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ Map Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x0D,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ Bow Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x0C,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ Compass Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x0F,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ Falling Ceiling Room Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x06,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ Basement Chest",
			type: LocationType.Chest,
			offset: 0x03,
			bitToCheck: 0x0B,
			area: Area.ForestTempleMQ
		),

		new LocationInformation(
			name: "Forest Temple MQ GS First Hallway",
			type: LocationType.Skulltula,
			offset: 0x03,
			bitToCheck: 0x01,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ GS Raised Island Courtyard",
			type: LocationType.Skulltula,
			offset: 0x03,
			bitToCheck: 0x00,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ GS Level Island Courtyard",
			type: LocationType.Skulltula,
			offset: 0x03,
			bitToCheck: 0x02,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ GS Well",
			type: LocationType.Skulltula,
			offset: 0x03,
			bitToCheck: 0x03,
			area: Area.ForestTempleMQ
		),
		new LocationInformation(
			name: "Forest Temple MQ GS Block Push Room",
			type: LocationType.Skulltula,
			offset: 0x03,
			bitToCheck: 0x04,
			area: Area.ForestTempleMQ
		),

		new LocationInformation(
			name: "Forest Temple Phantom Ganon Heart",
			type: LocationType.BossItem,
			offset: 0x14,
			bitToCheck: default,
			area: Area.ForestTempleMQ
		),

		// Hyrule Field
		new LocationInformation(
			name: "HF Ocarina of Time Item",
			type: LocationType.Event,
			offset: 0x04,
			bitToCheck: 0x03,
			area: Area.HyruleField
		),
		new LocationInformation(
			name: "HF Near Market Grotto Chest",
			type: LocationType.Chest,
			offset: 0x3E,
			bitToCheck: 0x00,
			area: Area.HyruleField
		),
		new LocationInformation(
			name: "HF Tektite Grotto Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x3E,
			bitToCheck: 0x01,
			area: Area.HyruleField
		),
		new LocationInformation(
			name: "HF Southeast Grotto Chest",
			type: LocationType.Chest,
			offset: 0x3E,
			bitToCheck: 0x02,
			area: Area.HyruleField
		),
		new LocationInformation(
			name: "HF Open Grotto Chest",
			type: LocationType.Chest,
			offset: 0x3E,
			bitToCheck: 0x03,
			area: Area.HyruleField
		),
		new LocationInformation(
			name: "HF Cow Grotto Cow",
			type: LocationType.Cow,
			offset: 0x3E,
			bitToCheck: 0x19,
			area: Area.HyruleField
		),

		new LocationInformation(
			name: "Song from Ocarina of Time",
			type: LocationType.Event,
			offset: 0xA,
			bitToCheck: 0x9,
			area: Area.HyruleField
		),

		new LocationInformation(
			name: "HF Deku Scrub Grotto",
			type: LocationType.GetInfo,
			offset: 0x00,
			bitToCheck: 0x03,
			area: Area.HyruleField
		),
		new LocationInformation(
			name: "HF Deku Scrub Grotto",
			type: LocationType.Scrubsanity,
			offset: 0x10,
			bitToCheck: 0x03,
			area: Area.HyruleField
		),

		new LocationInformation(
			name: "HF GS Cow Grotto",
			type: LocationType.Skulltula,
			offset: 0x0A,
			bitToCheck: 0x00,
			area: Area.HyruleField
		),
		new LocationInformation(
			name: "HF GS Near Kak Grotto",
			type: LocationType.Skulltula,
			offset: 0x0A,
			bitToCheck: 0x01,
			area: Area.HyruleField
		),

		// Lon Lon Ranch
		new LocationInformation(
			name: "LLR Talons Chickens",
			type: LocationType.GetInfo,
			offset: 0x01,
			bitToCheck: 0x02,
			area: Area.LonLonRanch
		),
		new LocationInformation(
			name: "LLR Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x4C,
			bitToCheck: 0x01,
			area: Area.LonLonRanch
		),
		new LocationInformation(
			name: "LLR Tower Left Cow",
			type: LocationType.Cow,
			offset: 0x4C,
			bitToCheck: 0x19,
			area: Area.LonLonRanch
		),
		new LocationInformation(
			name: "LLR Tower Right Cow",
			type: LocationType.Cow,
			offset: 0x4C,
			bitToCheck: 0x18,
			area: Area.LonLonRanch
		),

		new LocationInformation(
			name: "Song from Malon",
			type: LocationType.Event,
			offset: 0x5,
			bitToCheck: 0x8,
			area: Area.LonLonRanch
		),

		new LocationInformation(
			name: "LLR Deku Scrub Grotto Left",
			type: LocationType.Scrubsanity,
			offset: 0x26,
			bitToCheck: 0x01,
			area: Area.LonLonRanch
		),
		new LocationInformation(
			name: "LLR Deku Scrub Grotto Center",
			type: LocationType.Scrubsanity,
			offset: 0x26,
			bitToCheck: 0x04,
			area: Area.LonLonRanch
		),
		new LocationInformation(
			name: "LLR Deku Scrub Grotto Right",
			type: LocationType.Scrubsanity,
			offset: 0x26,
			bitToCheck: 0x06,
			area: Area.LonLonRanch
		),

		new LocationInformation(
			name: "LLR Stables Left Cow",
			type: LocationType.Cow,
			offset: 0x36,
			bitToCheck: 0x18,
			area: Area.LonLonRanch
		),
		new LocationInformation(
			name: "LLR Stables Right Cow",
			type: LocationType.Cow,
			offset: 0x36,
			bitToCheck: 0x19,
			area: Area.LonLonRanch
		),

		new LocationInformation(
			name: "LLR GS House Window",
			type: LocationType.Skulltula,
			offset: 0x0B,
			bitToCheck: 0x02,
			area: Area.LonLonRanch
		),
		new LocationInformation(
			name: "LLR GS Tree",
			type: LocationType.Skulltula,
			offset: 0x0B,
			bitToCheck: 0x03,
			area: Area.LonLonRanch
		),
		new LocationInformation(
			name: "LLR GS Rain Shed",
			type: LocationType.Skulltula,
			offset: 0x0B,
			bitToCheck: 0x01,
			area: Area.LonLonRanch
		),
		new LocationInformation(
			name: "LLR GS Back Wall",
			type: LocationType.Skulltula,
			offset: 0x0B,
			bitToCheck: 0x00,
			area: Area.LonLonRanch
		),

		// Market
		new LocationInformation(
			name: "Market Shooting Gallery Reward",
			type: LocationType.GetInfo,
			offset: 0x00,
			bitToCheck: 0x05,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market Bombchu Bowling First Prize",
			type: LocationType.GetInfo,
			offset: 0x03,
			bitToCheck: 0x01,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market Bombchu Bowling Second Prize",
			type: LocationType.GetInfo,
			offset: 0x03,
			bitToCheck: 0x02,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market Treasure Chest Game Reward",
			type: LocationType.GetInfo,
			offset: 0x02,
			bitToCheck: 0x03,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market Lost Dog",
			type: LocationType.InfoTable,
			offset: 0x33,
			bitToCheck: 0x01,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market 10 Big Poes",
			type: LocationType.BigPoeBottle,
			offset: default,
			bitToCheck: default,
			area: Area.Market
		),

		new LocationInformation(
			name: "Sheik at Temple",
			type: LocationType.Event,
			offset: 0x5,
			bitToCheck: 0x5,
			area: Area.Market
		),
		new LocationInformation(
			name: "ToT Light Arrows Cutscene",
			type: LocationType.Event,
			offset: 0x0C,
			bitToCheck: 0x04,
			area: Area.Market
		),

		new LocationInformation(
			name: "Market GS Guard House",
			type: LocationType.Skulltula,
			offset: 0x0E,
			bitToCheck: 0x03,
			area: Area.Market
		),

		new LocationInformation(
			name: "Market Bazaar Item 5",
			type: LocationType.Shop,
			offset: 0x04,
			bitToCheck: 0x00,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market Bazaar Item 6",
			type: LocationType.Shop,
			offset: 0x04,
			bitToCheck: 0x01,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market Bazaar Item 7",
			type: LocationType.Shop,
			offset: 0x04,
			bitToCheck: 0x02,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market Bazaar Item 8",
			type: LocationType.Shop,
			offset: 0x04,
			bitToCheck: 0x03,
			area: Area.Market
		),

		new LocationInformation(
			name: "Market Potion Shop Item 5",
			type: LocationType.Shop,
			offset: 0x08,
			bitToCheck: 0x00,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market Potion Shop Item 6",
			type: LocationType.Shop,
			offset: 0x08,
			bitToCheck: 0x01,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market Potion Shop Item 7",
			type: LocationType.Shop,
			offset: 0x08,
			bitToCheck: 0x02,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market Potion Shop Item 8",
			type: LocationType.Shop,
			offset: 0x08,
			bitToCheck: 0x03,
			area: Area.Market
		),

		new LocationInformation(
			name: "Market Bombchu Shop Item 5",
			type: LocationType.Shop,
			offset: 0x01,
			bitToCheck: 0x00,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market Bombchu Shop Item 6",
			type: LocationType.Shop,
			offset: 0x01,
			bitToCheck: 0x01,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market Bombchu Shop Item 7",
			type: LocationType.Shop,
			offset: 0x01,
			bitToCheck: 0x02,
			area: Area.Market
		),
		new LocationInformation(
			name: "Market Bombchu Shop Item 8",
			type: LocationType.Shop,
			offset: 0x01,
			bitToCheck: 0x03,
			area: Area.Market
		),

		// Hyrule Castle
		new LocationInformation(
			name: "HC Malon Egg",
			type: LocationType.Event,
			offset: 0x01,
			bitToCheck: 0x02,
			area: Area.HyruleCastle
		),
		new LocationInformation(
			name: "HC Zeldas Letter",
			type: LocationType.Event,
			offset: 0x04,
			bitToCheck: 0x00,
			area: Area.HyruleCastle
		),
		new LocationInformation(
			name: "HC Great Fairy Reward",
			type: LocationType.GetInfo,
			offset: 0x02,
			bitToCheck: 0x01,
			area: Area.HyruleCastle
		),
		new LocationInformation(
			name: "Song from Impa",
			type: LocationType.Event,
			offset: 0x05,
			bitToCheck: 0x09,
			area: Area.HyruleCastle
		),

		new LocationInformation(
			name: "HC GS Tree",
			type: LocationType.Skulltula,
			offset: 0x0E,
			bitToCheck: 0x02,
			area: Area.HyruleCastle
		),
		new LocationInformation(
			name: "HC GS Storms Grotto",
			type: LocationType.Skulltula,
			offset: 0x0E,
			bitToCheck: 0x01,
			area: Area.HyruleCastle
		),

		// Kakariko Village
		new LocationInformation(
			name: "Kak Anju as Child",
			type: LocationType.GetInfo,
			offset: 0x00,
			bitToCheck: 0x04,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak Anju as Adult",
			type: LocationType.GetInfo,
			offset: 0x04,
			bitToCheck: 0x04,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak Impas House Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x37,
			bitToCheck: 0x01,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak Windmill Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x48,
			bitToCheck: 0x01,
			area: Area.KakarikoVillage
		),

		new LocationInformation(
			name: "Kak Man on Roof",
			type: LocationType.GetInfo,
			offset: 0x03,
			bitToCheck: 0x05,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak Open Grotto Chest",
			type: LocationType.Chest,
			offset: 0x3E,
			bitToCheck: 0x08,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak Redead Grotto Chest",
			type: LocationType.Chest,
			offset: 0x3E,
			bitToCheck: 0x0A,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak Shooting Gallery Reward",
			type: LocationType.GetInfo,
			offset: 0x00,
			bitToCheck: 0x06,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak 10 Gold Skulltula Reward",
			type: LocationType.Event,
			offset: 0x0D,
			bitToCheck: 0x0A,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak 20 Gold Skulltula Reward",
			type: LocationType.Event,
			offset: 0x0D,
			bitToCheck: 0x0B,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak 30 Gold Skulltula Reward",
			type: LocationType.Event,
			offset: 0x0D,
			bitToCheck: 0x0C,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak 40 Gold Skulltula Reward",
			type: LocationType.Event,
			offset: 0x0D,
			bitToCheck: 0x0D,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak 50 Gold Skulltula Reward",
			type: LocationType.Event,
			offset: 0x0D,
			bitToCheck: 0x0E,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak Impas House Cow",
			type: LocationType.Cow,
			offset: 0x37,
			bitToCheck: 0x18,
			area: Area.KakarikoVillage
		),

		new LocationInformation(
			name: "Song from Windmill",
			type: LocationType.Event,
			offset: 0x05,
			bitToCheck: 0x0B,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Sheik in Kakariko",
			type: LocationType.Event,
			offset: 0x05,
			bitToCheck: 0x04,
			area: Area.KakarikoVillage
		),

		new LocationInformation(
			name: "Kak GS Tree",
			type: LocationType.Skulltula,
			offset: 0x10,
			bitToCheck: 0x05,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak GS Near Gate Guard",
			type: LocationType.Skulltula,
			offset: 0x10,
			bitToCheck: 0x01,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak GS Watchtower",
			type: LocationType.Skulltula,
			offset: 0x10,
			bitToCheck: 0x02,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak GS Skulltula House",
			type: LocationType.Skulltula,
			offset: 0x10,
			bitToCheck: 0x04,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak GS House Under Construction",
			type: LocationType.Skulltula,
			offset: 0x10,
			bitToCheck: 0x03,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak GS Above Impas House",
			type: LocationType.Skulltula,
			offset: 0x10,
			bitToCheck: 0x06,
			area: Area.KakarikoVillage
		),

		new LocationInformation(
			name: "Kak Bazaar Item 5",
			type: LocationType.Shop,
			offset: 0x07,
			bitToCheck: 0x00,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak Bazaar Item 6",
			type: LocationType.Shop,
			offset: 0x07,
			bitToCheck: 0x01,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak Bazaar Item 7",
			type: LocationType.Shop,
			offset: 0x07,
			bitToCheck: 0x02,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak Bazaar Item 8",
			type: LocationType.Shop,
			offset: 0x07,
			bitToCheck: 0x03,
			area: Area.KakarikoVillage
		),

		new LocationInformation(
			name: "Kak Potion Shop Item 5",
			type: LocationType.Shop,
			offset: 0x03,
			bitToCheck: 0x00,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak Potion Shop Item 6",
			type: LocationType.Shop,
			offset: 0x03,
			bitToCheck: 0x01,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak Potion Shop Item 7",
			type: LocationType.Shop,
			offset: 0x03,
			bitToCheck: 0x02,
			area: Area.KakarikoVillage
		),
		new LocationInformation(
			name: "Kak Potion Shop Item 8",
			type: LocationType.Shop,
			offset: 0x03,
			bitToCheck: 0x03,
			area: Area.KakarikoVillage
		),

		// Graveyard
		new LocationInformation(
			name: "Graveyard Shield Grave Chest",
			type: LocationType.Chest,
			offset: 0x40,
			bitToCheck: 0x00,
			area: Area.Graveyard
		),
		new LocationInformation(
			name: "Graveyard Heart Piece Grave Chest",
			type: LocationType.Chest,
			offset: 0x3F,
			bitToCheck: 0x00,
			area: Area.Graveyard
		),
		new LocationInformation(
			name: "Graveyard Royal Familys Tomb Chest",
			type: LocationType.Chest,
			offset: 0x41,
			bitToCheck: 0x00,
			area: Area.Graveyard
		),
		new LocationInformation(
			name: "Graveyard Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x53,
			bitToCheck: 0x04,
			area: Area.Graveyard
		),
		new LocationInformation(
			name: "Graveyard Dampe Gravedigging Tour",
			type: LocationType.GroundItem,
			offset: 0x53,
			bitToCheck: 0x08,
			area: Area.Graveyard
		),
		new LocationInformation(
			name: "Graveyard Dampe Race Hookshot Chest",
			type: LocationType.Chest,
			offset: 0x48,
			bitToCheck: 0x00,
			area: Area.Graveyard
		),
		new LocationInformation(
			name: "Graveyard Dampe Race Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x48,
			bitToCheck: 0x07,
			area: Area.Graveyard
		),

		new LocationInformation(
			name: "Song from Royal Familys Tomb",
			type: LocationType.Event,
			offset: 0x05,
			bitToCheck: 0x0A,
			area: Area.Graveyard
		),

		new LocationInformation(
			name: "Graveyard GS Bean Patch",
			type: LocationType.Skulltula,
			offset: 0x10,
			bitToCheck: 0x00,
			area: Area.Graveyard
		),
		new LocationInformation(
			name: "Graveyard GS Wall",
			type: LocationType.Skulltula,
			offset: 0x10,
			bitToCheck: 0x07,
			area: Area.Graveyard
		),

		// Bottom of the Well
		new LocationInformation(
			name: "Bottom of the Well Front Left Fake Wall Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x08,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well Front Center Bombable Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x02,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well Back Left Bombable Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x04,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well Underwater Left Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x09,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well Freestanding Key",
			type: LocationType.GroundItem,
			offset: 0x08,
			bitToCheck: 0x01,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well Compass Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x01,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well Center Skulltula Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x0E,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well Right Bottom Fake Wall Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x05,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well Fire Keese Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x0A,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well Like Like Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x0C,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well Map Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x07,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well Underwater Front Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x10,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well Invisible Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x14,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well Lens of Truth Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x03,
			area: Area.BottomOfTheWell
		),

		new LocationInformation(
			name: "Bottom of the Well GS West Inner Room",
			type: LocationType.Skulltula,
			offset: 0x08,
			bitToCheck: 0x02,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well GS East Inner Room",
			type: LocationType.Skulltula,
			offset: 0x08,
			bitToCheck: 0x01,
			area: Area.BottomOfTheWell
		),
		new LocationInformation(
			name: "Bottom of the Well GS Like Like Cage",
			type: LocationType.Skulltula,
			offset: 0x08,
			bitToCheck: 0x00,
			area: Area.BottomOfTheWell
		),

		// Bottom of the Well MQ
		new LocationInformation(
			name: "Bottom of the Well MQ Map Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x03,
			area: Area.BottomOfTheWellMQ
		),
		new LocationInformation(
			name: "Bottom of the Well MQ East Inner Room Freestanding Key",
			type: LocationType.GroundItem,
			offset: 0x08,
			bitToCheck: 0x01,
			area: Area.BottomOfTheWellMQ
		),
		new LocationInformation(
			name: "Bottom of the Well MQ Compass Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x02,
			area: Area.BottomOfTheWellMQ
		),
		new LocationInformation(
			name: "Bottom of the Well MQ Dead Hand Freestanding Key",
			type: LocationType.GroundItem,
			offset: 0x08,
			bitToCheck: 0x02,
			area: Area.BottomOfTheWellMQ
		),
		new LocationInformation(
			name: "Bottom of the Well MQ Lens of Truth Chest",
			type: LocationType.Chest,
			offset: 0x08,
			bitToCheck: 0x01,
			area: Area.BottomOfTheWellMQ
		),

		new LocationInformation(
			name: "Bottom of the Well MQ GS Coffin Room",
			type: LocationType.Skulltula,
			offset: 0x08,
			bitToCheck: 0x02,
			area: Area.BottomOfTheWellMQ
		),
		new LocationInformation(
			name: "Bottom of the Well MQ GS West Inner Room",
			type: LocationType.Skulltula,
			offset: 0x08,
			bitToCheck: 0x01,
			area: Area.BottomOfTheWellMQ
		),
		new LocationInformation(
			name: "Bottom of the Well MQ GS Basement",
			type: LocationType.Skulltula,
			offset: 0x08,
			bitToCheck: 0x00,
			area: Area.BottomOfTheWellMQ
		),

		// Shadow Temple
		new LocationInformation(
			name: "Shadow Temple Map Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x01,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Hover Boots Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x07,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Compass Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x03,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Early Silver Rupee Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x02,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Invisible Blades Visible Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x0C,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Invisible Blades Invisible Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x16,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Falling Spikes Lower Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x05,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Falling Spikes Upper Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x06,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Falling Spikes Switch Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x04,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Invisible Spikes Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x09,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Freestanding Key",
			type: LocationType.GroundItem,
			offset: 0x07,
			bitToCheck: 0x01,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Wind Hint Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x15,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple After Wind Enemy Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x08,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple After Wind Hidden Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x14,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Spike Walls Left Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x0A,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Boss Key Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x0B,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple Invisible Floormaster Chest",
			type: LocationType.Chest,
			offset: 0x07,
			bitToCheck: 0x0D,
			area: Area.ShadowTemple
		),

		new LocationInformation(
			name: "Shadow Temple GS Invisible Blades Room",
			type: LocationType.Skulltula,
			offset: 0x07,
			bitToCheck: 0x03,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple GS Falling Spikes Room",
			type: LocationType.Skulltula,
			offset: 0x07,
			bitToCheck: 0x01,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple GS Single Giant Pot",
			type: LocationType.Skulltula,
			offset: 0x07,
			bitToCheck: 0x00,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple GS Near Ship",
			type: LocationType.Skulltula,
			offset: 0x07,
			bitToCheck: 0x04,
			area: Area.ShadowTemple
		),
		new LocationInformation(
			name: "Shadow Temple GS Triple Giant Pot",
			type: LocationType.Skulltula,
			offset: 0x07,
			bitToCheck: 0x02,
			area: Area.ShadowTemple
		),

		new LocationInformation(
			name: "Shadow Temple Bongo Bongo Heart",
			type: LocationType.BossItem,
			offset: 0x18,
			bitToCheck: default,
			area: Area.ShadowTemple
		),

		// Shadow Temple MQ
		new LocationInformation(
			name: "Shadow Temple MQ Early Gibdos Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0x3,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Map Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0x2,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Near Ship Invisible Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0xE,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Compass Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0x1,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Hover Boots Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0x7,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Invisible Blades Invisible Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0x16,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Invisible Blades Visible Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0xC,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Beamos Silver Rupees Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0xF,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Falling Spikes Lower Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0x5,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Falling Spikes Upper Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0x6,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Falling Spikes Switch Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0x4,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Invisible Spikes Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0x9,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Stalfos Room Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0x10,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Wind Hint Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0x15,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ After Wind Hidden Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0x14,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ After Wind Enemy Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0x8,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Boss Key Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0xB,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Spike Walls Left Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0xA,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Freestanding Key",
			type: LocationType.GroundItem,
			offset: 0x7,
			bitToCheck: 0x6,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ Bomb Flower Chest",
			type: LocationType.Chest,
			offset: 0x7,
			bitToCheck: 0xD,
			area: Area.ShadowTempleMQ
		),

		new LocationInformation(
			name: "Shadow Temple MQ GS Falling Spikes Room",
			type: LocationType.Skulltula,
			offset: 0x7,
			bitToCheck: 0x1,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ GS Wind Hint Room",
			type: LocationType.Skulltula,
			offset: 0x7,
			bitToCheck: 0x0,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ GS After Wind",
			type: LocationType.Skulltula,
			offset: 0x7,
			bitToCheck: 0x3,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ GS After Ship",
			type: LocationType.Skulltula,
			offset: 0x7,
			bitToCheck: 0x4,
			area: Area.ShadowTempleMQ
		),
		new LocationInformation(
			name: "Shadow Temple MQ GS Near Boss",
			type: LocationType.Skulltula,
			offset: 0x7,
			bitToCheck: 0x2,
			area: Area.ShadowTempleMQ
		),

		new LocationInformation(
			name: "Shadow Temple Bongo Bongo Heart",
			type: LocationType.BossItem,
			offset: 0x18,
			bitToCheck: default,
			area: Area.ShadowTempleMQ
		),

		// Death Mountain Trail
		new LocationInformation(
			name: "DMT Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x60,
			bitToCheck: 0x1E,
			area: Area.DeathMountainTrail
		),
		new LocationInformation(
			name: "DMT Chest",
			type: LocationType.Chest,
			offset: 0x60,
			bitToCheck: 0x01,
			area: Area.DeathMountainTrail
		),
		new LocationInformation(
			name: "DMT Storms Grotto Chest",
			type: LocationType.Chest,
			offset: 0x3E,
			bitToCheck: 0x17,
			area: Area.DeathMountainTrail
		),
		new LocationInformation(
			name: "DMT Great Fairy Reward",
			type: LocationType.TrailGreatFairy,
			offset: 0x3B,
			bitToCheck: 0x18,
			area: Area.DeathMountainTrail
		),
		new LocationInformation(
			name: "DMT Biggoron",
			type: LocationType.BiggoronSword,
			offset: default,
			bitToCheck: default,
			area: Area.DeathMountainTrail
		),
		new LocationInformation(
			name: "DMT Cow Grotto Cow",
			type: LocationType.Cow,
			offset: 0x3E,
			bitToCheck: 0x18,
			area: Area.DeathMountainTrail
		),

		new LocationInformation(
			name: "DMT GS Near Kak",
			type: LocationType.Skulltula,
			offset: 0x0F,
			bitToCheck: 0x2,
			area: Area.DeathMountainTrail
		),
		new LocationInformation(
			name: "DMT GS Bean Patch",
			type: LocationType.Skulltula,
			offset: 0x0F,
			bitToCheck: 0x1,
			area: Area.DeathMountainTrail
		),
		new LocationInformation(
			name: "DMT GS Above Dodongos Cavern",
			type: LocationType.Skulltula,
			offset: 0x0F,
			bitToCheck: 0x3,
			area: Area.DeathMountainTrail
		),
		new LocationInformation(
			name: "DMT GS Falling Rocks Path",
			type: LocationType.Skulltula,
			offset: 0x0F,
			bitToCheck: 0x4,
			area: Area.DeathMountainTrail
		),

		// Goron City
		new LocationInformation(
			name: "GC Darunias Joy",
			type: LocationType.Event,
			offset: 0x3,
			bitToCheck: 0x6,
			area: Area.GoronCity
		),
		new LocationInformation(
			name: "GC Pot Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x62,
			bitToCheck: 0x1F,
			area: Area.GoronCity
		),
		new LocationInformation(
			name: "GC Rolling Goron as Child",
			type: LocationType.InfoTable,
			offset: 0x22,
			bitToCheck: 0x6,
			area: Area.GoronCity
		),
		new LocationInformation(
			name: "GC Rolling Goron as Adult",
			type: LocationType.InfoTable,
			offset: 0x20,
			bitToCheck: 0x1,
			area: Area.GoronCity
		),
		new LocationInformation(
			name: "GC Medigoron",
			type: LocationType.Medigoron,
			offset: 0x62,
			bitToCheck: 0x1,
			area: Area.GoronCity
		),
		new LocationInformation(
			name: "GC Maze Left Chest",
			type: LocationType.Chest,
			offset: 0x62,
			bitToCheck: 0x00,
			area: Area.GoronCity
		),
		new LocationInformation(
			name: "GC Maze Right Chest",
			type: LocationType.Chest,
			offset: 0x62,
			bitToCheck: 0x01,
			area: Area.GoronCity
		),
		new LocationInformation(
			name: "GC Maze Center Chest",
			type: LocationType.Chest,
			offset: 0x62,
			bitToCheck: 0x02,
			area: Area.GoronCity
		),

		new LocationInformation(
			name: "GC Deku Scrub Grotto Left",
			type: LocationType.Scrubsanity,
			offset: 0x25,
			bitToCheck: 0x1,
			area: Area.GoronCity
		),
		new LocationInformation(
			name: "GC Deku Scrub Grotto Center",
			type: LocationType.Scrubsanity,
			offset: 0x25,
			bitToCheck: 0x4,
			area: Area.GoronCity
		),
		new LocationInformation(
			name: "GC Deku Scrub Grotto Right",
			type: LocationType.Scrubsanity,
			offset: 0x25,
			bitToCheck: 0x6,
			area: Area.GoronCity
		),

		new LocationInformation(
			name: "GC GS Center Platform",
			type: LocationType.Skulltula,
			offset: 0x0F,
			bitToCheck: 0x5,
			area: Area.GoronCity
		),
		new LocationInformation(
			name: "GC GS Boulder Maze",
			type: LocationType.Skulltula,
			offset: 0x0F,
			bitToCheck: 0x6,
			area: Area.GoronCity
		),

		new LocationInformation(
			name: "GC Shop Item 5",
			type: LocationType.Shop,
			offset: 0x5,
			bitToCheck: 0x0,
			area: Area.GoronCity
		),
		new LocationInformation(
			name: "GC Shop Item 6",
			type: LocationType.Shop,
			offset: 0x5,
			bitToCheck: 0x1,
			area: Area.GoronCity
		),
		new LocationInformation(
			name: "GC Shop Item 7",
			type: LocationType.Shop,
			offset: 0x5,
			bitToCheck: 0x2,
			area: Area.GoronCity
		),
		new LocationInformation(
			name: "GC Shop Item 8",
			type: LocationType.Shop,
			offset: 0x5,
			bitToCheck: 0x3,
			area: Area.GoronCity
		),

		// Death Mountain Crater
		new LocationInformation(
			name: "DMC Volcano Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x61,
			bitToCheck: 0x08,
			area: Area.DeathMountainCrater
		),
		new LocationInformation(
			name: "DMC Wall Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x61,
			bitToCheck: 0x02,
			area: Area.DeathMountainCrater
		),
		new LocationInformation(
			name: "DMC Upper Grotto Chest",
			type: LocationType.Chest,
			offset: 0x3E,
			bitToCheck: 0x1A,
			area: Area.DeathMountainCrater
		),
		new LocationInformation(
			name: "DMC Great Fairy Reward",
			type: LocationType.CraterGreatFairy,
			offset: 0x3B,
			bitToCheck: 0x10,
			area: Area.DeathMountainCrater
		),

		new LocationInformation(
			name: "Sheik in Crater",
			type: LocationType.Event,
			offset: 0x5,
			bitToCheck: 0x1,
			area: Area.DeathMountainCrater
		),

		new LocationInformation(
			name: "DMC Deku Scrub",
			type: LocationType.Scrubsanity,
			offset: 0x61,
			bitToCheck: 0x6,
			area: Area.DeathMountainCrater
		),
		new LocationInformation(
			name: "DMC Deku Scrub Grotto Left",
			type: LocationType.Scrubsanity,
			offset: 0x23,
			bitToCheck: 0x1,
			area: Area.DeathMountainCrater
		),
		new LocationInformation(
			name: "DMC Deku Scrub Grotto Center",
			type: LocationType.Scrubsanity,
			offset: 0x23,
			bitToCheck: 0x4,
			area: Area.DeathMountainCrater
		),
		new LocationInformation(
			name: "DMC Deku Scrub Grotto Right",
			type: LocationType.Scrubsanity,
			offset: 0x23,
			bitToCheck: 0x6,
			area: Area.DeathMountainCrater
		),

		new LocationInformation(
			name: "DMC GS Crate",
			type: LocationType.Skulltula,
			offset: 0x0F,
			bitToCheck: 0x7,
			area: Area.DeathMountainCrater
		),
		new LocationInformation(
			name: "DMC GS Bean Patch",
			type: LocationType.Skulltula,
			offset: 0x0F,
			bitToCheck: 0x0,
			area: Area.DeathMountainCrater
		),

		// Dodongos Cavern
		new LocationInformation(
			name: "Dodongos Cavern Map Chest",
			type: LocationType.Chest,
			offset: 0x01,
			bitToCheck: 0x8,
			area: Area.DodongosCavern
		),
		new LocationInformation(
			name: "Dodongos Cavern Compass Chest",
			type: LocationType.Chest,
			offset: 0x01,
			bitToCheck: 0x5,
			area: Area.DodongosCavern
		),
		new LocationInformation(
			name: "Dodongos Cavern Bomb Flower Platform Chest",
			type: LocationType.Chest,
			offset: 0x01,
			bitToCheck: 0x6,
			area: Area.DodongosCavern
		),
		new LocationInformation(
			name: "Dodongos Cavern Bomb Bag Chest",
			type: LocationType.Chest,
			offset: 0x01,
			bitToCheck: 0x4,
			area: Area.DodongosCavern
		),
		new LocationInformation(
			name: "Dodongos Cavern End of Bridge Chest",
			type: LocationType.Chest,
			offset: 0x01,
			bitToCheck: 0xA,
			area: Area.DodongosCavern
		),

		new LocationInformation(
			name: "Dodongos Cavern Deku Scrub Lobby",
			type: LocationType.Scrubsanity,
			offset: 0x1,
			bitToCheck: 0x5,
			area: Area.DodongosCavern
		),
		new LocationInformation(
			name: "Dodongos Cavern Deku Scrub Side Room Near Dodongos",
			type: LocationType.Scrubsanity,
			offset: 0x1,
			bitToCheck: 0x2,
			area: Area.DodongosCavern
		),
		new LocationInformation(
			name: "Dodongos Cavern Deku Scrub Near Bomb Bag Left",
			type: LocationType.Scrubsanity,
			offset: 0x1,
			bitToCheck: 0x1,
			area: Area.DodongosCavern
		),
		new LocationInformation(
			name: "Dodongos Cavern Deku Scrub Near Bomb Bag Right",
			type: LocationType.Scrubsanity,
			offset: 0x1,
			bitToCheck: 0x4,
			area: Area.DodongosCavern
		),

		new LocationInformation(
			name: "Dodongos Cavern GS Side Room Near Lower Lizalfos",
			type: LocationType.Skulltula,
			offset: 0x01,
			bitToCheck: 0x4,
			area: Area.DodongosCavern
		),
		new LocationInformation(
			name: "Dodongos Cavern GS Scarecrow",
			type: LocationType.Skulltula,
			offset: 0x01,
			bitToCheck: 0x1,
			area: Area.DodongosCavern
		),
		new LocationInformation(
			name: "Dodongos Cavern GS Alcove Above Stairs",
			type: LocationType.Skulltula,
			offset: 0x01,
			bitToCheck: 0x2,
			area: Area.DodongosCavern
		),
		new LocationInformation(
			name: "Dodongos Cavern GS Vines Above Stairs",
			type: LocationType.Skulltula,
			offset: 0x01,
			bitToCheck: 0x0,
			area: Area.DodongosCavern
		),
		new LocationInformation(
			name: "Dodongos Cavern GS Back Room",
			type: LocationType.Skulltula,
			offset: 0x01,
			bitToCheck: 0x3,
			area: Area.DodongosCavern
		),

		new LocationInformation(
			name: "Dodongos Cavern Boss Room Chest",
			type: LocationType.Chest,
			offset: 0x12,
			bitToCheck: 0x0,
			area: Area.DodongosCavern
		),
		new LocationInformation(
			name: "Dodongos Cavern King Dodongo Heart",
			type: LocationType.BossItem,
			offset: 0x12,
			bitToCheck: default,
			area: Area.DodongosCavern
		),

		// Dodongos Cavern MQ
		new LocationInformation(
			name: "Dodongos Cavern MQ Map Chest",
			type: LocationType.Chest,
			offset: 0x1,
			bitToCheck: 0x0,
			area: Area.DodongosCavernMQ
		),
		new LocationInformation(
			name: "Dodongos Cavern MQ Bomb Bag Chest",
			type: LocationType.Chest,
			offset: 0x1,
			bitToCheck: 0x4,
			area: Area.DodongosCavernMQ
		),
		new LocationInformation(
			name: "Dodongos Cavern MQ Torch Puzzle Room Chest",
			type: LocationType.Chest,
			offset: 0x1,
			bitToCheck: 0x3,
			area: Area.DodongosCavernMQ
		),
		new LocationInformation(
			name: "Dodongos Cavern MQ Larvae Room Chest",
			type: LocationType.Chest,
			offset: 0x1,
			bitToCheck: 0x2,
			area: Area.DodongosCavernMQ
		),
		new LocationInformation(
			name: "Dodongos Cavern MQ Compass Chest",
			type: LocationType.Chest,
			offset: 0x1,
			bitToCheck: 0x5,
			area: Area.DodongosCavernMQ
		),
		new LocationInformation(
			name: "Dodongos Cavern MQ Under Grave Chest",
			type: LocationType.Chest,
			offset: 0x1,
			bitToCheck: 0x1,
			area: Area.DodongosCavernMQ
		),

		new LocationInformation(
			name: "Dodongos Cavern MQ Deku Scrub Lobby Front",
			type: LocationType.Scrubsanity,
			offset: 0x1,
			bitToCheck: 0x4,
			area: Area.DodongosCavernMQ
		),
		new LocationInformation(
			name: "Dodongos Cavern MQ Deku Scrub Lobby Rear",
			type: LocationType.Scrubsanity,
			offset: 0x1,
			bitToCheck: 0x2,
			area: Area.DodongosCavernMQ
		),
		new LocationInformation(
			name: "Dodongos Cavern MQ Deku Scrub Side Room Near Lower Lizalfos",
			type: LocationType.Scrubsanity,
			offset: 0x1,
			bitToCheck: 0x8,
			area: Area.DodongosCavernMQ
		),
		new LocationInformation(
			name: "Dodongos Cavern MQ Deku Scrub Staircase",
			type: LocationType.Scrubsanity,
			offset: 0x1,
			bitToCheck: 0x5,
			area: Area.DodongosCavernMQ
		),

		new LocationInformation(
			name: "Dodongos Cavern MQ GS Scrub Room",
			type: LocationType.Skulltula,
			offset: 0x1,
			bitToCheck: 0x1,
			area: Area.DodongosCavernMQ
		),
		new LocationInformation(
			name: "Dodongos Cavern MQ GS Larvae Room",
			type: LocationType.Skulltula,
			offset: 0x1,
			bitToCheck: 0x4,
			area: Area.DodongosCavernMQ
		),
		new LocationInformation(
			name: "Dodongos Cavern MQ GS Lizalfos Room",
			type: LocationType.Skulltula,
			offset: 0x1,
			bitToCheck: 0x2,
			area: Area.DodongosCavernMQ
		),
		new LocationInformation(
			name: "Dodongos Cavern MQ GS Song of Time Block Room",
			type: LocationType.Skulltula,
			offset: 0x1,
			bitToCheck: 0x3,
			area: Area.DodongosCavernMQ
		),
		new LocationInformation(
			name: "Dodongos Cavern MQ GS Back Area",
			type: LocationType.Skulltula,
			offset: 0x1,
			bitToCheck: 0x0,
			area: Area.DodongosCavernMQ
		),

		new LocationInformation(
			name: "Dodongos Cavern Boss Room Chest",
			type: LocationType.Chest,
			offset: 0x12,
			bitToCheck: 0x0,
			area: Area.DodongosCavernMQ
		),
		new LocationInformation(
			name: "Dodongos Cavern King Dodongo Heart",
			type: LocationType.BossItem,
			offset: 0x12,
			bitToCheck: default,
			area: Area.DodongosCavernMQ
		),

		// Fire Temple
		new LocationInformation(
			name: "Fire Temple Near Boss Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x01,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple Flare Dancer Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x00,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple Boss Key Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x0C,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple Big Lava Room Lower Open Door Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x04,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple Big Lava Room Blocked Door Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x02,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple Boulder Maze Lower Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x03,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple Boulder Maze Side Room Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x08,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple Map Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x0A,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple Boulder Maze Shortcut Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x0B,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple Boulder Maze Upper Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x06,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple Scarecrow Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x0D,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple Compass Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x07,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple Megaton Hammer Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x05,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple Highest Goron Chest",
			type: LocationType.Chest,
			offset: 0x04,
			bitToCheck: 0x09,
			area: Area.FireTemple
		),

		new LocationInformation(
			name: "Fire Temple GS Boss Key Loop",
			type: LocationType.Skulltula,
			offset: 0x04,
			bitToCheck: 0x1,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple GS Song of Time Room",
			type: LocationType.Skulltula,
			offset: 0x04,
			bitToCheck: 0x0,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple GS Boulder Maze",
			type: LocationType.Skulltula,
			offset: 0x04,
			bitToCheck: 0x2,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple GS Scarecrow Climb",
			type: LocationType.Skulltula,
			offset: 0x04,
			bitToCheck: 0x4,
			area: Area.FireTemple
		),
		new LocationInformation(
			name: "Fire Temple GS Scarecrow Top",
			type: LocationType.Skulltula,
			offset: 0x04,
			bitToCheck: 0x3,
			area: Area.FireTemple
		),

		new LocationInformation(
			name: "Fire Temple Volvagia Heart",
			type: LocationType.BossItem,
			offset: 0x15,
			bitToCheck: default,
			area: Area.FireTemple
		),

		// Fire Temple MQ
		new LocationInformation(
			name: "Fire Temple MQ Map Room Side Chest",
			type: LocationType.Chest,
			offset: 0x4,
			bitToCheck: 0x2,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ Megaton Hammer Chest",
			type: LocationType.Chest,
			offset: 0x4,
			bitToCheck: 0x0,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ Map Chest",
			type: LocationType.Chest,
			offset: 0x4,
			bitToCheck: 0xC,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ Near Boss Chest",
			type: LocationType.Chest,
			offset: 0x4,
			bitToCheck: 0x7,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ Big Lava Room Blocked Door Chest",
			type: LocationType.Chest,
			offset: 0x4,
			bitToCheck: 0x1,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ Boss Key Chest",
			type: LocationType.Chest,
			offset: 0x4,
			bitToCheck: 0x4,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ Lizalfos Maze Side Room Chest",
			type: LocationType.Chest,
			offset: 0x4,
			bitToCheck: 0x8,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ Compass Chest",
			type: LocationType.Chest,
			offset: 0x4,
			bitToCheck: 0xB,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ Lizalfos Maze Upper Chest",
			type: LocationType.Chest,
			offset: 0x4,
			bitToCheck: 0x6,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ Lizalfos Maze Lower Chest",
			type: LocationType.Chest,
			offset: 0x4,
			bitToCheck: 0x3,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ Freestanding Key",
			type: LocationType.GroundItem,
			offset: 0x4,
			bitToCheck: 0x1C,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ Chest On Fire",
			type: LocationType.Chest,
			offset: 0x4,
			bitToCheck: 0x5,
			area: Area.FireTempleMQ
		),

		new LocationInformation(
			name: "Fire Temple MQ GS Big Lava Room Open Door",
			type: LocationType.Skulltula,
			offset: 0x4,
			bitToCheck: 0x0,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ GS Skull On Fire",
			type: LocationType.Skulltula,
			offset: 0x4,
			bitToCheck: 0x2,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ GS Flame Maze Center",
			type: LocationType.Skulltula,
			offset: 0x4,
			bitToCheck: 0x3,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ GS Flame Maze Side Room",
			type: LocationType.Skulltula,
			offset: 0x4,
			bitToCheck: 0x4,
			area: Area.FireTempleMQ
		),
		new LocationInformation(
			name: "Fire Temple MQ GS Above Flame Maze",
			type: LocationType.Skulltula,
			offset: 0x4,
			bitToCheck: 0x1,
			area: Area.FireTempleMQ
		),

		new LocationInformation(
			name: "Fire Temple Volvagia Heart",
			type: LocationType.BossItem,
			offset: 0x15,
			bitToCheck: default,
			area: Area.FireTempleMQ
		),

		// Zora's River
		new LocationInformation(
			name: "ZR Magic Bean Salesman",
			type: LocationType.BeanSale,
			offset: 0x54,
			bitToCheck: 0x1,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR Open Grotto Chest",
			type: LocationType.Chest,
			offset: 0x3E,
			bitToCheck: 0x09,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR Frogs in the Rain",
			type: LocationType.Event,
			offset: 0xD,
			bitToCheck: 0x6,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR Frogs Ocarina Game",
			type: LocationType.Event,
			offset: 0xD,
			bitToCheck: 0x0,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR Near Open Grotto Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x54,
			bitToCheck: 0x04,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR Near Domain Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x54,
			bitToCheck: 0x0B,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR Deku Scrub Grotto Front",
			type: LocationType.Scrubsanity,
			offset: 0x15,
			bitToCheck: 0x9,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR Deku Scrub Grotto Rear",
			type: LocationType.Scrubsanity,
			offset: 0x15,
			bitToCheck: 0x8,
			area: Area.ZorasRiver
		),

		new LocationInformation(
			name: "ZR Frogs Zeldas Lullaby",
			type: LocationType.Event,
			offset: 0xD,
			bitToCheck: 0x1,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR Frogs Eponas Song",
			type: LocationType.Event,
			offset: 0xD,
			bitToCheck: 0x2,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR Frogs Suns Song",
			type: LocationType.Event,
			offset: 0xD,
			bitToCheck: 0x3,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR Frogs Sarias Song",
			type: LocationType.Event,
			offset: 0xD,
			bitToCheck: 0x4,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR Frogs Song of Time",
			type: LocationType.Event,
			offset: 0xD,
			bitToCheck: 0x5,
			area: Area.ZorasRiver
		),

		new LocationInformation(
			name: "ZR GS Tree",
			type: LocationType.Skulltula,
			offset: 0x11,
			bitToCheck: 0x1,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR GS Ladder",
			type: LocationType.Skulltula,
			offset: 0x11,
			bitToCheck: 0x0,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR GS Near Raised Grottos",
			type: LocationType.Skulltula,
			offset: 0x11,
			bitToCheck: 0x4,
			area: Area.ZorasRiver
		),
		new LocationInformation(
			name: "ZR GS Above Bridge",
			type: LocationType.Skulltula,
			offset: 0x11,
			bitToCheck: 0x3,
			area: Area.ZorasRiver
		),

		// Zora's Domain
		new LocationInformation(
			name: "ZD Diving Minigame",
			type: LocationType.Event,
			offset: 0x3,
			bitToCheck: 0x8,
			area: Area.ZorasDomain
		),
		new LocationInformation(
			name: "ZD Chest",
			type: LocationType.Chest,
			offset: 0x58,
			bitToCheck: 0x00,
			area: Area.ZorasDomain
		),
		new LocationInformation(
			name: "ZD King Zora Thawed",
			type: LocationType.InfoTable,
			offset: 0x26,
			bitToCheck: 0x1,
			area: Area.ZorasDomain
		),
		new LocationInformation(
			name: "ZD GS Frozen Waterfall",
			type: LocationType.Skulltula,
			offset: 0x11,
			bitToCheck: 0x6,
			area: Area.ZorasDomain
		),

		new LocationInformation(
			name: "ZD Shop Item 5",
			type: LocationType.Shop,
			offset: 0x2,
			bitToCheck: 0x0,
			area: Area.ZorasDomain
		),
		new LocationInformation(
			name: "ZD Shop Item 6",
			type: LocationType.Shop,
			offset: 0x2,
			bitToCheck: 0x1,
			area: Area.ZorasDomain
		),
		new LocationInformation(
			name: "ZD Shop Item 7",
			type: LocationType.Shop,
			offset: 0x2,
			bitToCheck: 0x2,
			area: Area.ZorasDomain
		),
		new LocationInformation(
			name: "ZD Shop Item 8",
			type: LocationType.Shop,
			offset: 0x2,
			bitToCheck: 0x3,
			area: Area.ZorasDomain
		),

		// Zora's Fountain
		new LocationInformation(
			name: "ZF Great Fairy Reward",
			type: LocationType.GetInfo,
			offset: 0x2,
			bitToCheck: 0x0,
			area: Area.ZorasFountain
		),
		new LocationInformation(
			name: "ZF Iceberg Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x59,
			bitToCheck: 0x01,
			area: Area.ZorasFountain
		),
		new LocationInformation(
			name: "ZF Bottom Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x59,
			bitToCheck: 0x14,
			area: Area.ZorasFountain
		),
		new LocationInformation(
			name: "ZF GS Above the Log",
			type: LocationType.Skulltula,
			offset: 0x11,
			bitToCheck: 0x2,
			area: Area.ZorasFountain
		),
		new LocationInformation(
			name: "ZF GS Tree",
			type: LocationType.Skulltula,
			offset: 0x11,
			bitToCheck: 0x7,
			area: Area.ZorasFountain
		),
		new LocationInformation(
			name: "ZF GS Hidden Cave",
			type: LocationType.Skulltula,
			offset: 0x11,
			bitToCheck: 0x5,
			area: Area.ZorasFountain
		),

		// Jabu Jabu's Belly
		new LocationInformation(
			name: "Jabu Jabus Belly Boomerang Chest",
			type: LocationType.Chest,
			offset: 0x02,
			bitToCheck: 0x01,
			area: Area.JabuJabusBelly
		),
		new LocationInformation(
			name: "Jabu Jabus Belly Map Chest",
			type: LocationType.Chest,
			offset: 0x02,
			bitToCheck: 0x02,
			area: Area.JabuJabusBelly
		),
		new LocationInformation(
			name: "Jabu Jabus Belly Compass Chest",
			type: LocationType.Chest,
			offset: 0x02,
			bitToCheck: 0x04,
			area: Area.JabuJabusBelly
		),

		new LocationInformation(
			name: "Jabu Jabus Belly Deku Scrub",
			type: LocationType.Scrubsanity,
			offset: 0x02,
			bitToCheck: 0x1,
			area: Area.JabuJabusBelly
		),

		new LocationInformation(
			name: "Jabu Jabus Belly GS Water Switch Room",
			type: LocationType.Skulltula,
			offset: 0x02,
			bitToCheck: 0x3,
			area: Area.JabuJabusBelly
		),
		new LocationInformation(
			name: "Jabu Jabus Belly GS Lobby Basement Lower",
			type: LocationType.Skulltula,
			offset: 0x02,
			bitToCheck: 0x0,
			area: Area.JabuJabusBelly
		),
		new LocationInformation(
			name: "Jabu Jabus Belly GS Lobby Basement Upper",
			type: LocationType.Skulltula,
			offset: 0x02,
			bitToCheck: 0x1,
			area: Area.JabuJabusBelly
		),
		new LocationInformation(
			name: "Jabu Jabus Belly GS Near Boss",
			type: LocationType.Skulltula,
			offset: 0x02,
			bitToCheck: 0x2,
			area: Area.JabuJabusBelly
		),

		new LocationInformation(
			name: "Jabu Jabus Belly Barinade Heart",
			type: LocationType.BossItem,
			offset: 0x13,
			bitToCheck: default,
			area: Area.JabuJabusBelly
		),

		// Jabu Jabu's Belly MQ
		new LocationInformation(
			name: "Jabu Jabus Belly MQ Map Chest",
			type: LocationType.Chest,
			offset: 0x2,
			bitToCheck: 0x3,
			area: Area.JabuJabusBellyMQ
		),
		new LocationInformation(
			name: "Jabu Jabus Belly MQ First Room Side Chest",
			type: LocationType.Chest,
			offset: 0x2,
			bitToCheck: 0x5,
			area: Area.JabuJabusBellyMQ
		),
		new LocationInformation(
			name: "Jabu Jabus Belly MQ Second Room Lower Chest",
			type: LocationType.Chest,
			offset: 0x2,
			bitToCheck: 0x2,
			area: Area.JabuJabusBellyMQ
		),
		new LocationInformation(
			name: "Jabu Jabus Belly MQ Compass Chest",
			type: LocationType.Chest,
			offset: 0x2,
			bitToCheck: 0x0,
			area: Area.JabuJabusBellyMQ
		),
		new LocationInformation(
			name: "Jabu Jabus Belly MQ Basement Near Switches Chest",
			type: LocationType.Chest,
			offset: 0x2,
			bitToCheck: 0x8,
			area: Area.JabuJabusBellyMQ
		),
		new LocationInformation(
			name: "Jabu Jabus Belly MQ Basement Near Vines Chest",
			type: LocationType.Chest,
			offset: 0x2,
			bitToCheck: 0x4,
			area: Area.JabuJabusBellyMQ
		),
		new LocationInformation(
			name: "Jabu Jabus Belly MQ Boomerang Room Small Chest",
			type: LocationType.Chest,
			offset: 0x2,
			bitToCheck: 0x1,
			area: Area.JabuJabusBellyMQ
		),
		new LocationInformation(
			name: "Jabu Jabus Belly MQ Boomerang Chest",
			type: LocationType.Chest,
			offset: 0x2,
			bitToCheck: 0x6,
			area: Area.JabuJabusBellyMQ
		),
		new LocationInformation(
			name: "Jabu Jabus Belly MQ Falling Like Like Room Chest",
			type: LocationType.Chest,
			offset: 0x2,
			bitToCheck: 0x9,
			area: Area.JabuJabusBellyMQ
		),
		new LocationInformation(
			name: "Jabu Jabus Belly MQ Second Room Upper Chest",
			type: LocationType.Chest,
			offset: 0x2,
			bitToCheck: 0x7,
			area: Area.JabuJabusBellyMQ
		),
		new LocationInformation(
			name: "Jabu Jabus Belly MQ Near Boss Chest",
			type: LocationType.Chest,
			offset: 0x2,
			bitToCheck: 0xA,
			area: Area.JabuJabusBellyMQ
		),

		new LocationInformation(
			name: "Jabu Jabus Belly MQ Cow",
			type: LocationType.Cow,
			offset: 0x2,
			bitToCheck: 0x18,
			area: Area.JabuJabusBellyMQ
		),

		new LocationInformation(
			name: "Jabu Jabus Belly MQ GS Boomerang Chest Room",
			type: LocationType.Skulltula,
			offset: 0x2,
			bitToCheck: 0x0,
			area: Area.JabuJabusBellyMQ
		),
		new LocationInformation(
			name: "Jabu Jabus Belly MQ GS Tailpasaran Room",
			type: LocationType.Skulltula,
			offset: 0x2,
			bitToCheck: 0x2,
			area: Area.JabuJabusBellyMQ
		),
		new LocationInformation(
			name: "Jabu Jabus Belly MQ GS Invisible Enemies Room",
			type: LocationType.Skulltula,
			offset: 0x2,
			bitToCheck: 0x3,
			area: Area.JabuJabusBellyMQ
		),
		new LocationInformation(
			name: "Jabu Jabus Belly MQ GS Near Boss",
			type: LocationType.Skulltula,
			offset: 0x2,
			bitToCheck: 0x1,
			area: Area.JabuJabusBellyMQ
		),

		new LocationInformation(
			name: "Jabu Jabus Belly Barinade Heart",
			type: LocationType.BossItem,
			offset: 0x13,
			bitToCheck: default,
			area: Area.JabuJabusBellyMQ
		),

		// Ice Cavern
		new LocationInformation(
			name: "Ice Cavern Map Chest",
			type: LocationType.Chest,
			offset: 0x09,
			bitToCheck: 0x00,
			area: Area.IceCavern
		),
		new LocationInformation(
			name: "Ice Cavern Compass Chest",
			type: LocationType.Chest,
			offset: 0x09,
			bitToCheck: 0x01,
			area: Area.IceCavern
		),
		new LocationInformation(
			name: "Ice Cavern Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x09,
			bitToCheck: 0x01,
			area: Area.IceCavern
		),
		new LocationInformation(
			name: "Ice Cavern Iron Boots Chest",
			type: LocationType.Chest,
			offset: 0x09,
			bitToCheck: 0x02,
			area: Area.IceCavern
		),

		new LocationInformation(
			name: "Sheik in Ice Cavern",
			type: LocationType.Event,
			offset: 0x5,
			bitToCheck: 0x2,
			area: Area.IceCavern
		),

		new LocationInformation(
			name: "Ice Cavern GS Spinning Scythe Room",
			type: LocationType.Skulltula,
			offset: 0x09,
			bitToCheck: 0x1,
			area: Area.IceCavern
		),
		new LocationInformation(
			name: "Ice Cavern GS Heart Piece Room",
			type: LocationType.Skulltula,
			offset: 0x09,
			bitToCheck: 0x2,
			area: Area.IceCavern
		),
		new LocationInformation(
			name: "Ice Cavern GS Push Block Room",
			type: LocationType.Skulltula,
			offset: 0x09,
			bitToCheck: 0x0,
			area: Area.IceCavern
		),

		// Ice Cavern MQ
		new LocationInformation(
			name: "Ice Cavern MQ Map Chest",
			type: LocationType.Chest,
			offset: 0x09,
			bitToCheck: 0x01,
			area: Area.IceCavernMQ
		),
		new LocationInformation(
			name: "Ice Cavern MQ Compass Chest",
			type: LocationType.Chest,
			offset: 0x09,
			bitToCheck: 0x00,
			area: Area.IceCavernMQ
		),
		new LocationInformation(
			name: "Ice Cavern MQ Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x09,
			bitToCheck: 0x01,
			area: Area.IceCavernMQ
		),
		new LocationInformation(
			name: "Ice Cavern MQ Iron Boots Chest",
			type: LocationType.Chest,
			offset: 0x09,
			bitToCheck: 0x02,
			area: Area.IceCavernMQ
		),

		new LocationInformation(
			name: "Sheik in Ice Cavern",
			type: LocationType.Event,
			offset: 0x5,
			bitToCheck: 0x2,
			area: Area.IceCavernMQ
		),

		new LocationInformation(
			name: "Ice Cavern MQ GS Red Ice",
			type: LocationType.Skulltula,
			offset: 0x09,
			bitToCheck: 0x1,
			area: Area.IceCavernMQ
		),
		new LocationInformation(
			name: "Ice Cavern MQ GS Ice Block",
			type: LocationType.Skulltula,
			offset: 0x09,
			bitToCheck: 0x2,
			area: Area.IceCavernMQ
		),
		new LocationInformation(
			name: "Ice Cavern MQ GS Scarecrow",
			type: LocationType.Skulltula,
			offset: 0x09,
			bitToCheck: 0x0,
			area: Area.IceCavernMQ
		),

		// Lake Hylia
		new LocationInformation(
			name: "LH Underwater Item",
			type: LocationType.Event,
			offset: 0x3,
			bitToCheck: 0x1,
			area: Area.LakeHylia
		),
		new LocationInformation(
			name: "LH Child Fishing",
			type: LocationType.FishingChild,
			offset: default,
			bitToCheck: default,
			area: Area.LakeHylia
		),
		new LocationInformation(
			name: "LH Adult Fishing",
			type: LocationType.FishingAdult,
			offset: default,
			bitToCheck: default,
			area: Area.LakeHylia
		),
		new LocationInformation(
			name: "LH Lab Dive",
			type: LocationType.GetInfo,
			offset: 0x3,
			bitToCheck: 0x0,
			area: Area.LakeHylia
		),
		new LocationInformation(
			name: "LH Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x57,
			bitToCheck: 0x1E,
			area: Area.LakeHylia
		),
		new LocationInformation(
			name: "LH Sun",
			type: LocationType.FireArrows,
			offset: 0x57,
			bitToCheck: 0x0,
			area: Area.LakeHylia
		),
		new LocationInformation(
			name: "LH Deku Scrub Grotto Left",
			type: LocationType.Scrubsanity,
			offset: 0x19,
			bitToCheck: 0x1,
			area: Area.LakeHylia
		),
		new LocationInformation(
			name: "LH Deku Scrub Grotto Center",
			type: LocationType.Scrubsanity,
			offset: 0x19,
			bitToCheck: 0x4,
			area: Area.LakeHylia
		),
		new LocationInformation(
			name: "LH Deku Scrub Grotto Right",
			type: LocationType.Scrubsanity,
			offset: 0x19,
			bitToCheck: 0x6,
			area: Area.LakeHylia
		),

		new LocationInformation(
			name: "LH GS Lab Wall",
			type: LocationType.Skulltula,
			offset: 0x12,
			bitToCheck: 0x2,
			area: Area.LakeHylia
		),
		new LocationInformation(
			name: "LH GS Bean Patch",
			type: LocationType.Skulltula,
			offset: 0x12,
			bitToCheck: 0x0,
			area: Area.LakeHylia
		),
		new LocationInformation(
			name: "LH GS Small Island",
			type: LocationType.Skulltula,
			offset: 0x12,
			bitToCheck: 0x1,
			area: Area.LakeHylia
		),
		new LocationInformation(
			name: "LH GS Lab Crate",
			type: LocationType.Skulltula,
			offset: 0x12,
			bitToCheck: 0x3,
			area: Area.LakeHylia
		),
		new LocationInformation(
			name: "LH GS Tree",
			type: LocationType.Skulltula,
			offset: 0x12,
			bitToCheck: 0x4,
			area: Area.LakeHylia
		),

		// Water Temple		
		new LocationInformation(
			name: "Water Temple Compass Chest",
			type: LocationType.Chest,
			offset: 0x05,
			bitToCheck: 0x09,
			area: Area.WaterTemple
		),
		new LocationInformation(
			name: "Water Temple Map Chest",
			type: LocationType.Chest,
			offset: 0x05,
			bitToCheck: 0x02,
			area: Area.WaterTemple
		),
		new LocationInformation(
			name: "Water Temple Cracked Wall Chest",
			type: LocationType.Chest,
			offset: 0x05,
			bitToCheck: 0x00,
			area: Area.WaterTemple
		),
		new LocationInformation(
			name: "Water Temple Torches Chest",
			type: LocationType.Chest,
			offset: 0x05,
			bitToCheck: 0x01,
			area: Area.WaterTemple
		),
		new LocationInformation(
			name: "Water Temple Boss Key Chest",
			type: LocationType.Chest,
			offset: 0x05,
			bitToCheck: 0x05,
			area: Area.WaterTemple
		),
		new LocationInformation(
			name: "Water Temple Central Pillar Chest",
			type: LocationType.Chest,
			offset: 0x05,
			bitToCheck: 0x06,
			area: Area.WaterTemple
		),
		new LocationInformation(
			name: "Water Temple Central Bow Target Chest",
			type: LocationType.Chest,
			offset: 0x05,
			bitToCheck: 0x08,
			area: Area.WaterTemple
		),
		new LocationInformation(
			name: "Water Temple Longshot Chest",
			type: LocationType.Chest,
			offset: 0x05,
			bitToCheck: 0x07,
			area: Area.WaterTemple
		),
		new LocationInformation(
			name: "Water Temple River Chest",
			type: LocationType.Chest,
			offset: 0x05,
			bitToCheck: 0x03,
			area: Area.WaterTemple
		),
		new LocationInformation(
			name: "Water Temple Dragon Chest",
			type: LocationType.Chest,
			offset: 0x05,
			bitToCheck: 0x0A,
			area: Area.WaterTemple
		),

		new LocationInformation(
			name: "Water Temple GS Behind Gate",
			type: LocationType.Skulltula,
			offset: 0x05,
			bitToCheck: 0x0,
			area: Area.WaterTemple
		),
		new LocationInformation(
			name: "Water Temple GS Near Boss Key Chest",
			type: LocationType.Skulltula,
			offset: 0x05,
			bitToCheck: 0x3,
			area: Area.WaterTemple
		),
		new LocationInformation(
			name: "Water Temple GS Central Pillar",
			type: LocationType.Skulltula,
			offset: 0x05,
			bitToCheck: 0x2,
			area: Area.WaterTemple
		),
		new LocationInformation(
			name: "Water Temple GS Falling Platform Room",
			type: LocationType.Skulltula,
			offset: 0x05,
			bitToCheck: 0x1,
			area: Area.WaterTemple
		),
		new LocationInformation(
			name: "Water Temple GS River",
			type: LocationType.Skulltula,
			offset: 0x05,
			bitToCheck: 0x4,
			area: Area.WaterTemple
		),

		new LocationInformation(
			name: "Water Temple Morpha Heart",
			type: LocationType.BossItem,
			offset: 0x16,
			bitToCheck: default,
			area: Area.WaterTemple
		),

		// Water Temple MQ
		new LocationInformation(
			name: "Water Temple MQ Longshot Chest",
			type: LocationType.Chest,
			offset: 0x5,
			bitToCheck: 0x0,
			area: Area.WaterTempleMQ
		),
		new LocationInformation(
			name: "Water Temple MQ Map Chest",
			type: LocationType.Chest,
			offset: 0x5,
			bitToCheck: 0x2,
			area: Area.WaterTempleMQ
		),
		new LocationInformation(
			name: "Water Temple MQ Compass Chest",
			type: LocationType.Chest,
			offset: 0x5,
			bitToCheck: 0x1,
			area: Area.WaterTempleMQ
		),
		new LocationInformation(
			name: "Water Temple MQ Central Pillar Chest",
			type: LocationType.Chest,
			offset: 0x5,
			bitToCheck: 0x6,
			area: Area.WaterTempleMQ
		),
		new LocationInformation(
			name: "Water Temple MQ Boss Key Chest",
			type: LocationType.Chest,
			offset: 0x5,
			bitToCheck: 0x5,
			area: Area.WaterTempleMQ
		),
		new LocationInformation(
			name: "Water Temple MQ Freestanding Key",
			type: LocationType.GroundItem,
			offset: 0x5,
			bitToCheck: 0x1,
			area: Area.WaterTempleMQ
		),

		new LocationInformation(
			name: "Water Temple MQ GS Lizalfos Hallway",
			type: LocationType.Skulltula,
			offset: 0x5,
			bitToCheck: 0x0,
			area: Area.WaterTempleMQ
		),
		new LocationInformation(
			name: "Water Temple MQ GS Before Upper Water Switch",
			type: LocationType.Skulltula,
			offset: 0x5,
			bitToCheck: 0x2,
			area: Area.WaterTempleMQ
		),
		new LocationInformation(
			name: "Water Temple MQ GS River",
			type: LocationType.Skulltula,
			offset: 0x5,
			bitToCheck: 0x1,
			area: Area.WaterTempleMQ
		),
		new LocationInformation(
			name: "Water Temple MQ GS Freestanding Key Area",
			type: LocationType.Skulltula,
			offset: 0x5,
			bitToCheck: 0x3,
			area: Area.WaterTempleMQ
		),
		new LocationInformation(
			name: "Water Temple MQ GS Triple Wall Torch",
			type: LocationType.Skulltula,
			offset: 0x5,
			bitToCheck: 0x4,
			area: Area.WaterTempleMQ
		),

		new LocationInformation(
			name: "Water Temple Morpha Heart",
			type: LocationType.BossItem,
			offset: 0x16,
			bitToCheck: default,
			area: Area.WaterTemple
		),

		// Gerudo Valley
		new LocationInformation(
			name: "GV Crate Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x5A,
			bitToCheck: 0x2,
			area: Area.GerudoValley
		),
		new LocationInformation(
			name: "GV Waterfall Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x5A,
			bitToCheck: 0x1,
			area: Area.GerudoValley
		),
		new LocationInformation(
			name: "GV Chest",
			type: LocationType.Chest,
			offset: 0x5A,
			bitToCheck: 0x00,
			area: Area.GerudoValley
		),
		new LocationInformation(
			name: "GV Deku Scrub Grotto Front",
			type: LocationType.Scrubsanity,
			offset: 0x1A,
			bitToCheck: 0x9,
			area: Area.GerudoValley
		),
		new LocationInformation(
			name: "GV Deku Scrub Grotto Rear",
			type: LocationType.Scrubsanity,
			offset: 0x1A,
			bitToCheck: 0x8,
			area: Area.GerudoValley
		),
		new LocationInformation(
			name: "GV Cow",
			type: LocationType.Cow,
			offset: 0x5A,
			bitToCheck: 0x18,
			area: Area.GerudoValley
		),

		new LocationInformation(
			name: "GV GS Small Bridge",
			type: LocationType.Skulltula,
			offset: 0x13,
			bitToCheck: 0x1,
			area: Area.GerudoValley
		),
		new LocationInformation(
			name: "GV GS Bean Patch",
			type: LocationType.Skulltula,
			offset: 0x13,
			bitToCheck: 0x0,
			area: Area.GerudoValley
		),
		new LocationInformation(
			name: "GV GS Behind Tent",
			type: LocationType.Skulltula,
			offset: 0x13,
			bitToCheck: 0x3,
			area: Area.GerudoValley
		),
		new LocationInformation(
			name: "GV GS Pillar",
			type: LocationType.Skulltula,
			offset: 0x13,
			bitToCheck: 0x2,
			area: Area.GerudoValley
		),

		// Gerudo Fortress
		new LocationInformation(
			name: "Hideout 1 Torch Jail Gerudo Key",
			type: LocationType.GroundItem,
			offset: 0xC,
			bitToCheck: 0xC,
			area: Area.GerudoFortress
		),
		new LocationInformation(
			name: "Hideout 2 Torches Jail Gerudo Key",
			type: LocationType.GroundItem,
			offset: 0xC,
			bitToCheck: 0xF,
			area: Area.GerudoFortress
		),
		new LocationInformation(
			name: "Hideout 3 Torches Jail Gerudo Key",
			type: LocationType.GroundItem,
			offset: 0xC,
			bitToCheck: 0xA,
			area: Area.GerudoFortress
		),
		new LocationInformation(
			name: "Hideout 4 Torches Jail Gerudo Key",
			type: LocationType.GroundItem,
			offset: 0xC,
			bitToCheck: 0xE,
			area: Area.GerudoFortress
		),
		new LocationInformation(
			name: "Hideout Gerudo Membership Card",
			type: LocationType.MembershipCardCheck,
			offset: 0xC,
			bitToCheck: 0x2,
			area: Area.GerudoFortress
		),
		new LocationInformation(
			name: "GF Chest",
			type: LocationType.Chest,
			offset: 0x5D,
			bitToCheck: 0x0,
			area: Area.GerudoFortress
		),
		new LocationInformation(
			name: "GF HBA 1000 Points",
			type: LocationType.InfoTable,
			offset: 0x33,
			bitToCheck: 0x0,
			area: Area.GerudoFortress
		),
		new LocationInformation(
			name: "GF HBA 1500 Points",
			type: LocationType.GetInfo,
			offset: 0x0,
			bitToCheck: 0x7,
			area: Area.GerudoFortress
		),
		new LocationInformation(
			name: "GF GS Top Floor",
			type: LocationType.Skulltula,
			offset: 0x14,
			bitToCheck: 0x1,
			area: Area.GerudoFortress
		),
		new LocationInformation(
			name: "GF GS Archery Range",
			type: LocationType.Skulltula,
			offset: 0x14,
			bitToCheck: 0x0,
			area: Area.GerudoFortress
		),

		// Gerudo Training Ground
		new LocationInformation(
			name: "Gerudo Training Ground Lobby Left Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x13,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Lobby Right Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x07,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Stalfos Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x00,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Before Heavy Block Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x11,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Heavy Block First Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x0F,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Heavy Block Second Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x0E,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Heavy Block Third Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x14,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Heavy Block Fourth Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x02,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Eye Statue Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x03,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Near Scarecrow Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x04,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Hammer Room Clear Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x12,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Hammer Room Switch Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x10,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Freestanding Key",
			type: LocationType.GroundItem,
			offset: 0x0B,
			bitToCheck: 0x1,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Maze Right Central Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x05,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Maze Right Side Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x08,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Underwater Silver Rupee Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x0D,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Beamos Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x01,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Hidden Ceiling Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x0B,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Maze Path First Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x06,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Maze Path Second Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x0A,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Maze Path Third Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x09,
			area: Area.GerudoTrainingGround
		),
		new LocationInformation(
			name: "Gerudo Training Ground Maze Path Final Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x0C,
			area: Area.GerudoTrainingGround
		),

		// Gerudo Training Ground MQ
		new LocationInformation(
			name: "Gerudo Training Ground MQ Lobby Left Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x13,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Lobby Right Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x07,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ First Iron Knuckle Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x00,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Before Heavy Block Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x11,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Heavy Block Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x02,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Eye Statue Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x03,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Ice Arrows Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x04,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Second Iron Knuckle Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x12,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Flame Circle Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x0E,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Maze Right Central Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x05,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Maze Right Side Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x08,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Underwater Silver Rupee Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x0D,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Dinolfos Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x01,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Hidden Ceiling Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x0B,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Maze Path First Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x06,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Maze Path Second Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x0A,
			area: Area.GerudoTrainingGroundMQ
		),
		new LocationInformation(
			name: "Gerudo Training Ground MQ Maze Path Third Chest",
			type: LocationType.Chest,
			offset: 0x0B,
			bitToCheck: 0x09,
			area: Area.GerudoTrainingGroundMQ
		),

		// Haunted Wasteland
		new LocationInformation(
			name: "Wasteland Bombchu Salesman",
			type: LocationType.BombchuSalesman,
			offset: 0x5E,
			bitToCheck: 0x01,
			area: Area.HauntedWasteland
		),
		new LocationInformation(
			name: "Wasteland Chest",
			type: LocationType.Chest,
			offset: 0x5E,
			bitToCheck: 0x00,
			area: Area.HauntedWasteland
		),
		new LocationInformation(
			name: "Wasteland GS",
			type: LocationType.Skulltula,
			offset: 0x15,
			bitToCheck: 0x1,
			area: Area.HauntedWasteland
		),

		// Desert Colossus
		new LocationInformation(
			name: "Colossus Great Fairy Reward",
			type: LocationType.GetInfo,
			offset: 0x2,
			bitToCheck: 0x2,
			area: Area.DesertColossus
		),
		new LocationInformation(
			name: "Colossus Freestanding PoH",
			type: LocationType.GroundItem,
			offset: 0x5C,
			bitToCheck: 0xD,
			area: Area.DesertColossus
		),

		new LocationInformation(
			name: "Sheik at Colossus",
			type: LocationType.Event,
			offset: 0xA,
			bitToCheck: 0xC,
			area: Area.DesertColossus
		),

		new LocationInformation(
			name: "Colossus Deku Scrub Grotto Front",
			type: LocationType.Scrubsanity,
			offset: 0x27,
			bitToCheck: 0x9,
			area: Area.DesertColossus
		),
		new LocationInformation(
			name: "Colossus Deku Scrub Grotto Rear",
			type: LocationType.Scrubsanity,
			offset: 0x27,
			bitToCheck: 0x8,
			area: Area.DesertColossus
		),

		new LocationInformation(
			name: "Colossus GS Bean Patch",
			type: LocationType.Skulltula,
			offset: 0x15,
			bitToCheck: 0x0,
			area: Area.DesertColossus
		),
		new LocationInformation(
			name: "Colossus GS Tree",
			type: LocationType.Skulltula,
			offset: 0x15,
			bitToCheck: 0x3,
			area: Area.DesertColossus
		),
		new LocationInformation(
			name: "Colossus GS Hill",
			type: LocationType.Skulltula,
			offset: 0x15,
			bitToCheck: 0x2,
			area: Area.DesertColossus
		),

		// Spirit Temple
		new LocationInformation(
			name: "Spirit Temple Child Bridge Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x08,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Child Early Torches Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x00,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Child Climb North Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x06,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Child Climb East Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x0C,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Map Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x03,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Sun Block Room Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x01,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Silver Gauntlets Chest",
			type: LocationType.Chest,
			offset: 0x5C,
			bitToCheck: 0x0B,
			area: Area.SpiritTemple
		),

		new LocationInformation(
			name: "Spirit Temple Compass Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x04,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Early Adult Right Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x07,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple First Mirror Left Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x0D,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple First Mirror Right Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x0E,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Statue Room Northeast Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x0F,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Statue Room Hand Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x02,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Near Four Armos Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x05,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Hallway Right Invisible Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x14,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Hallway Left Invisible Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x15,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Mirror Shield Chest",
			type: LocationType.Chest,
			offset: 0x5C,
			bitToCheck: 0x09,
			area: Area.SpiritTemple
		),

		new LocationInformation(
			name: "Spirit Temple Boss Key Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x0A,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple Topmost Chest",
			type: LocationType.Chest,
			offset: 0x06,
			bitToCheck: 0x12,
			area: Area.SpiritTemple
		),

		new LocationInformation(
			name: "Spirit Temple GS Metal Fence",
			type: LocationType.Skulltula,
			offset: 0x06,
			bitToCheck: 0x4,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple GS Sun on Floor Room",
			type: LocationType.Skulltula,
			offset: 0x06,
			bitToCheck: 0x3,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple GS Hall After Sun Block Room",
			type: LocationType.Skulltula,
			offset: 0x06,
			bitToCheck: 0x0,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple GS Lobby",
			type: LocationType.Skulltula,
			offset: 0x06,
			bitToCheck: 0x2,
			area: Area.SpiritTemple
		),
		new LocationInformation(
			name: "Spirit Temple GS Boulder Room",
			type: LocationType.Skulltula,
			offset: 0x06,
			bitToCheck: 0x1,
			area: Area.SpiritTemple
		),

		new LocationInformation(
			name: "Spirit Temple Twinrova Heart",
			type: LocationType.BossItem,
			offset: 0x17,
			bitToCheck: default,
			area: Area.SpiritTemple
		),

		// Spirit Temple MQ
		new LocationInformation(
			name: "Spirit Temple MQ Entrance Front Left Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x1A,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Entrance Back Right Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x1F,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Entrance Front Right Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x1B,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Entrance Back Left Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x1E,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Map Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x0,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Map Room Enemy Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x8,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Child Climb North Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x6,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Child Climb South Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0xC,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Compass Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x3,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Silver Block Hallway Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x1C,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Sun Block Room Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x1,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple Silver Gauntlets Chest",
			type: LocationType.Chest,
			offset: 0x5C,
			bitToCheck: 0xB,
			area: Area.SpiritTemple
		),

		new LocationInformation(
			name: "Spirit Temple MQ Child Hammer Switch Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x1D,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Statue Room Lullaby Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0xF,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Statue Room Invisible Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x2,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Leever Room Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x4,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Symphony Room Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x7,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Beamos Room Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x19,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Chest Switch Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x18,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Boss Key Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x5,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple Mirror Shield Chest",
			type: LocationType.Chest,
			offset: 0x5C,
			bitToCheck: 0x9,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ Mirror Puzzle Invisible Chest",
			type: LocationType.Chest,
			offset: 0x6,
			bitToCheck: 0x12,
			area: Area.SpiritTempleMQ
		),

		new LocationInformation(
			name: "Spirit Temple MQ GS Sun Block Room",
			type: LocationType.Skulltula,
			offset: 0x6,
			bitToCheck: 0x0,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ GS Leever Room",
			type: LocationType.Skulltula,
			offset: 0x6,
			bitToCheck: 0x1,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ GS Symphony Room",
			type: LocationType.Skulltula,
			offset: 0x6,
			bitToCheck: 0x3,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ GS Nine Thrones Room West",
			type: LocationType.Skulltula,
			offset: 0x6,
			bitToCheck: 0x2,
			area: Area.SpiritTempleMQ
		),
		new LocationInformation(
			name: "Spirit Temple MQ GS Nine Thrones Room North",
			type: LocationType.Skulltula,
			offset: 0x6,
			bitToCheck: 0x4,
			area: Area.SpiritTempleMQ
		),

		new LocationInformation(
			name: "Spirit Temple Twinrova Heart",
			type: LocationType.BossItem,
			offset: 0x17,
			bitToCheck: default,
			area: Area.SpiritTemple
		),

		// Outside Ganon's Castle
		new LocationInformation(
			name: "OGC Great Fairy Reward",
			type: LocationType.GreatFairy,
			offset: 0x3B,
			bitToCheck: 0x8,
			area: Area.OutsideGanonsCastle
		),
		new LocationInformation(
			name: "OGC GS",
			type: LocationType.Skulltula,
			offset: 0x0E,
			bitToCheck: 0x0,
			area: Area.OutsideGanonsCastle
		),

		// Ganon's Castle
		new LocationInformation(
			name: "Ganons Castle Forest Trial Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x09,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Water Trial Left Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x07,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Water Trial Right Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x06,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Shadow Trial Front Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x08,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Shadow Trial Golden Gauntlets Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x05,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Light Trial First Left Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x0C,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Light Trial Second Left Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x0B,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Light Trial Third Left Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x0D,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Light Trial First Right Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x0E,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Light Trial Second Right Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x0A,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Light Trial Third Right Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x0F,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Light Trial Invisible Enemies Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x10,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Light Trial Lullaby Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x11,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Spirit Trial Crystal Switch Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x12,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Spirit Trial Invisible Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x14,
			area: Area.GanonsCastle
		),

		new LocationInformation(
			name: "Ganons Castle Deku Scrub Left",
			type: LocationType.Scrubsanity,
			offset: 0x0D,
			bitToCheck: 0x9,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Deku Scrub Center-Left",
			type: LocationType.Scrubsanity,
			offset: 0x0D,
			bitToCheck: 0x6,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Deku Scrub Center-Right",
			type: LocationType.Scrubsanity,
			offset: 0x0D,
			bitToCheck: 0x4,
			area: Area.GanonsCastle
		),
		new LocationInformation(
			name: "Ganons Castle Deku Scrub Right",
			type: LocationType.Scrubsanity,
			offset: 0x0D,
			bitToCheck: 0x8,
			area: Area.GanonsCastle
		),

		new LocationInformation(
			name: "Ganons Tower Boss Key Chest",
			type: LocationType.Chest,
			offset: 0x0A,
			bitToCheck: 0x0B,
			area: Area.GanonsCastle
		),

		// Ganon's Castle MQ
		new LocationInformation(
			name: "Ganons Castle MQ Forest Trial Freestanding Key",
			type: LocationType.GroundItem,
			offset: 0x0D,
			bitToCheck: 0x1,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Forest Trial Eye Switch Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x2,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Forest Trial Frozen Eye Switch Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x3,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Water Trial Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x1,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Shadow Trial Bomb Flower Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x0,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Shadow Trial Eye Switch Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x5,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Light Trial Lullaby Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x4,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Spirit Trial First Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0xA,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Spirit Trial Invisible Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x14,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Spirit Trial Sun Front Left Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x9,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Spirit Trial Sun Back Left Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x8,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Spirit Trial Sun Back Right Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x7,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Spirit Trial Golden Gauntlets Chest",
			type: LocationType.Chest,
			offset: 0x0D,
			bitToCheck: 0x6,
			area: Area.GanonsCastleMQ
		),

		new LocationInformation(
			name: "Ganons Castle MQ Deku Scrub Left",
			type: LocationType.Scrubsanity,
			offset: 0x0D,
			bitToCheck: 0x9,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Deku Scrub Center-Left",
			type: LocationType.Scrubsanity,
			offset: 0x0D,
			bitToCheck: 0x6,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Deku Scrub Center",
			type: LocationType.Scrubsanity,
			offset: 0x0D,
			bitToCheck: 0x4,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Deku Scrub Center-Right",
			type: LocationType.Scrubsanity,
			offset: 0x0D,
			bitToCheck: 0x8,
			area: Area.GanonsCastleMQ
		),
		new LocationInformation(
			name: "Ganons Castle MQ Deku Scrub Right",
			type: LocationType.Scrubsanity,
			offset: 0x0D,
			bitToCheck: 0x1,
			area: Area.GanonsCastleMQ
		),

		new LocationInformation(
			name: "Ganons Tower Boss Key Chest",
			type: LocationType.Chest,
			offset: 0x0A,
			bitToCheck: 0x0B,
			area: Area.GanonsCastleMQ
		),
	];
}
