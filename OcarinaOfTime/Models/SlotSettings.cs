namespace OOT_AP_Client.OcarinaOfTime.Models;

public record SlotSettings
{
	public bool ShuffleScrubs { get; }

	public SlotSettings(bool shuffleScrubs)
	{
		ShuffleScrubs = shuffleScrubs;
	}
}
