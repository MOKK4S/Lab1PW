namespace Lab6PW.Models;

public enum AnimalType
{
    None,
    Hyrax,
    Raccoon,
    Crocodile
}

public class GameSettings
{
    public int BoardWidth { get; set; } = 3;
    public int BoardHeight { get; set; } = 3;
    public int HyraxCount { get; set; } = 1;
    public int RaccoonCount { get; set; } = 2;
    public int CrocodileCount { get; set; } = 0;
}

public class ScoreEntry
{
    public string PlayerName { get; set; } = "Player";
    public double TimeInSeconds { get; set; }
    public System.DateTime Date { get; set; } = System.DateTime.Now;
}
