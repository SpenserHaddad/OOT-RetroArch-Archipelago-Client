using OOT_AP_Client.OcarinaOfTime.Enums;

namespace OOT_AP_Client.OcarinaOfTime.Models;

public class LocationInformation
{
	public string Name { get; }
	public LocationType Type { get; }
	public byte Offset { get; }
	public byte BitToCheck { get; }
	public Area Area { get; }

	public LocationInformation(
		string name,
		LocationType type,
		byte offset,
		byte bitToCheck,
		Area area
	)
	{
		Name = name;
		Type = type;
		Offset = offset;
		BitToCheck = bitToCheck;
		Area = area;
	}
}
