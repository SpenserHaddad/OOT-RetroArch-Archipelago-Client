using OOT_AP_Client.Models;

namespace OOT_AP_Client.Data;

public static class AllLocationInformation
{
	public static LocationInformation[] LocationInformationArray { get; } =
	[
		// Kokiri Forest
		new LocationInformation(
			name: "KF Midos Top Left Chest",
			type: LocationType.Chest,
			offset: 0x28,
			bitToCheck: 0x00
		),
		new LocationInformation(
			name: "KF Midos Top Right Chest",
			type: LocationType.Chest,
			offset: 0x28,
			bitToCheck: 0x01
		),
		new LocationInformation(
			name: "KF Midos Bottom Left Chest",
			type: LocationType.Chest,
			offset: 0x28,
			bitToCheck: 0x02
		),
		new LocationInformation(
			name: "KF Midos Bottom Right Chest",
			type: LocationType.Chest,
			offset: 0x28,
			bitToCheck: 0x03
		),
		new LocationInformation(
			name: "KF Kokiri Sword Chest",
			type: LocationType.Chest,
			offset: 0x55,
			bitToCheck: 0x00
		),
		new LocationInformation(
			name: "KF Storms Grotto Chest",
			type: LocationType.Chest,
			offset: 0x3E,
			bitToCheck: 0x0C
		),

		new LocationInformation(
			name: "KF Links House Cow",
			type: LocationType.Cow,
			offset: 0x34,
			bitToCheck: 0x18
		),

		new LocationInformation(
			name: "KF GS Know It All House",
			type: LocationType.Skulltula,
			offset: 0x0C,
			bitToCheck: 0x01
		),
		new LocationInformation(
			name: "KF GS Bean Patch",
			type: LocationType.Skulltula,
			offset: 0x0C,
			bitToCheck: 0x00
		),
		new LocationInformation(
			name: "KF GS House of Twins",
			type: LocationType.Skulltula,
			offset: 0x0C,
			bitToCheck: 0x02
		),

		new LocationInformation(
			name: "KF Shop Item 5",
			type: LocationType.Shop,
			offset: 0x06,
			bitToCheck: 0x00
		),
		new LocationInformation(
			name: "KF Shop Item 6",
			type: LocationType.Shop,
			offset: 0x06,
			bitToCheck: 0x01
		),
		new LocationInformation(
			name: "KF Shop Item 7",
			type: LocationType.Shop,
			offset: 0x06,
			bitToCheck: 0x02
		),
		new LocationInformation(
			name: "KF Shop Item 8",
			type: LocationType.Shop,
			offset: 0x06,
			bitToCheck: 0x03
		),

		new LocationInformation(
			name: "KF Shop Blue Rupee",
			type: LocationType.GroundItem,
			offset: 0x2D,
			bitToCheck: 0x01
		),
	];
}
