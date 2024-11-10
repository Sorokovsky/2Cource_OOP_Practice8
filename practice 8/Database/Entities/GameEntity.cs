namespace Practice_8.Database.Entities;

[Serializable]
public class GameEntity : BaseEntity
{
    public string Name { get; set; }
    public DateTime PlayedAt { get; set; }
    public int FirstTeamId { get; set; }
    public int SecondTeamId { get; set; }
    public int StadiumId { get; set; }

    public GameEntity(string name, DateOnly playedAtDate, TimeOnly playedAtTime)
    {
        Name = name;
        PlayedAt = new DateTime(playedAtDate, playedAtTime);
    }

    public GameEntity()
    {
        
    }
    
    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"Name: {Name}\n" +
               $"Played at date: {DateOnly.FromDateTime(PlayedAt)}\n" +
               $"Played at time: {TimeOnly.FromDateTime(PlayedAt)}";
    }

    public static GameEntity Enter()
    {
        Console.Write("Enter a name: ");
        var name = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a year of played at: ");
        var year = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter a number of month of played at: ");
        var month = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter a number of day of played at: ");
        var day = Convert.ToInt32(Console.ReadLine());
        var dateOnly = new DateOnly(year, month, day);
        Console.Write("Enter a hour of played at: ");
        var hour = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter a minutes of played at: ");
        var minutes = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter a seconds of played at: ");
        var seconds = Convert.ToInt32(Console.ReadLine());
        var timeOnly = new TimeOnly(hour, minutes, seconds);
        return new GameEntity(name, dateOnly, timeOnly);
    }
}