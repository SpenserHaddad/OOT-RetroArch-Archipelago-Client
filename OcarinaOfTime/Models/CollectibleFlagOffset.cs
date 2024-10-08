namespace OOT_AP_Client.OcarinaOfTime.Models;

public record CollectibleFlagOffset
{
	public required long ItemId { get; init; }
	public required long Offset { get; init; }
	public required long Flag { get; init; }
}
