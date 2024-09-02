namespace OOT_AP_Client.OcarinaOfTime.Models;

public record OOTClientSlotData
{
	public required bool ShuffleScrubs { get; init; }
	public required uint CollectibleOverridesFlagsAddress { get; init; }
	public required List<CollectibleFlagOffset> CollectibleFlagOffsets { get; init; }
}
