namespace OOT_AP_Client.OcarinaOfTime.Models;

public record OOTClientConnectionSettings
{
	public required string ArchipelagoHostName { get; init; }
	public required int ArchipelagoPort { get; init; }
	public required string SlotName { get; init; }

	public required string RetroarchHostName { get; init; }
	public required int RetroarchPort { get; init; }
}
