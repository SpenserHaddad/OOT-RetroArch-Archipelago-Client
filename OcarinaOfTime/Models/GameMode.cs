namespace OOT_AP_Client.OcarinaOfTime.Models;

public record GameMode
{
	public GameMode(string name, bool isInGame)
	{
		Name = name;
		IsInGame = isInGame;
	}

	public string Name { get; }
	public bool IsInGame { get; }
}
