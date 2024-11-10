using Practice_8.Database.Entities;

namespace Practice_8.Models;

public class TeamModel
{
    public string Name { get; private set; }
    public CoachModel? Coach { get; private set; }
    public List<PlayerModel>? Players { get; private set; }

    public TeamModel(TeamEntity entity, CoachModel? coach = null, List<PlayerModel>? players = null)
    {
        Name = entity.Name;
        Coach = coach;
        Players = players;
    }

    public override string ToString()
    {
        var playersList = string.Empty;
        var players = Players != null ? $"\n Players: \n" + Players.Aggregate(playersList, (current, player) => current + "\n" + player) : string.Empty;
        var coach = Coach != null ? $"\n{Coach}" : string.Empty;
        var result = $"Team: \n" +
                     $"Name: {Name}" +
                     $"{coach}" +
                     $"{players}";
        return result;
    }
}