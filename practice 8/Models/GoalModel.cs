using Practice_8.Database.Entities;

namespace Practice_8.Models;

public class GoalModel
{
    public int Count { get; private set; }
    public GameModel? Game { get; private set; }
    public PlayerModel? Player { get; private set; }

    public GoalModel(GoalEntity entity, GameModel? game = null, PlayerModel? player = null)
    {
        Count = entity.Count;
        Game = game;
        Player = player;
    }

    public override string ToString()
    {
        var game = Game != null ? $"\n{Game}" : string.Empty;
        var player = Player != null ? $"\n{Player}" : string.Empty;
        return $"Goals: \n" +
               $"Count of goals: {Count}\n" +
               $"{game}" +
               $"{player}";
    }
}