namespace OOT_AP_Client.OcarinaOfTime.Models;

public record GameMode
{
	public required string Name { get; init; }
	public required bool IsInGame { get; init; }
}
