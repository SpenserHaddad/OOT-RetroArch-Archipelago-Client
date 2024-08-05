namespace OOT_AP_Client;

public record CollectibleFlagOffset
{
	public uint ItemId { get; }
	public uint Offset { get; }
	public uint Flag { get; }

	public CollectibleFlagOffset(uint itemId, uint offset, uint flag)
	{
		ItemId = itemId;
		Offset = offset;
		Flag = flag;
	}
}
