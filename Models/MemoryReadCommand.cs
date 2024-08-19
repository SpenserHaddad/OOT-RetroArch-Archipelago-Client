namespace OOT_AP_Client.Models;

public record MemoryReadCommand
{
	public required long Address { get; init; }
	public required int NumberOfBytes { get; init; }
}
