using Practice_8.Database.Entities;

namespace Practice_8.Models;

public class GameModel
{
    public string Name { get; private set; }
    public DateOnly PlayedAtDate { get; private set; }
    public TimeOnly PlayedAtTime { get; private set; }
    public TeamModel? FirstTeam { get; private set; }
    public TeamModel? SecondTeam { get; private set; }
    public StadiumModel? Stadium { get; set; }

    public GameModel(GameEntity entity, TeamModel? firstTeam = null, TeamModel secondTeam = null, StadiumModel? stadium = null)
    {
        Name = entity.Name;
        PlayedAtDate = entity.PlayedAtDate;
        PlayedAtTime = entity.PlayedAtTime;
        FirstTeam = firstTeam;
        SecondTeam = secondTeam;
        Stadium = stadium;
    }

    public override string ToString()
    {
        var firstTeam = FirstTeam != null ? $"\nFirst {FirstTeam}" : string.Empty;
        var secondTeam = SecondTeam != null ? $"\nSecond {SecondTeam}" : string.Empty;
        var stadium = Stadium != null ? $"\n{Stadium}" : string.Empty;
        return $"Game: \n" +
               $"Name: {Name}\n" +
               $"Played at date: {PlayedAtDate.DayNumber}.{PlayedAtDate.Month}.{PlayedAtDate.Year}\n" +
               $"Played at time: {PlayedAtTime.Hour}:{PlayedAtTime.Minute}:{PlayedAtTime.Second}" +
               $"{firstTeam}" +
               $"{secondTeam}" +
               $"{stadium}";
    }
}