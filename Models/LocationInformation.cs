namespace OOT_AP_Client.Models;

public class LocationInformation
{
	public string Name { get; }
	public LocationType Type { get; }
	public byte Offset { get; }
	public byte BitToCheck { get; }

	public LocationInformation(string name, LocationType type, byte offset, byte bitToCheck)
	{
		Name = name;
		Type = type;
		Offset = offset;
		BitToCheck = bitToCheck;
	}
}
