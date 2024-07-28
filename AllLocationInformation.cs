namespace OOT_AP_Client;

public static class AllLocationInformation
{
	public static LocationInformation[] LocationInformations { get; } =
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
	];
}
