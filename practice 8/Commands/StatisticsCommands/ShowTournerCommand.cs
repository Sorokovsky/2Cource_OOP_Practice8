using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.StatisticsCommands;

public class ShowTournerCommand : Command 
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show tournier table.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        SimulateNotSimulated(database);
        var items = CollectTableItems(database);
        Console.WriteLine("|   Team  |  Mark  |");
        foreach (var item in items)
        {
            Console.WriteLine($"|{item.Team.Name}|{item.Mark}|");
        }
    }

    private void SimulateNotSimulated(DbContext database)
    {
        var games = database.Games.List.ToList();
        foreach (var game in games)
        {
            if (IsNeedToSimulate(game, database))
            {
                var max = 10;
                var goalCount = new Random().Next(1, max);
                for (int i = 0; i < goalCount; i++)
                {
                    var goal = new GoalEntity();
                    goal.GameId = game.Id;
                    var players = database.Players.List
                        .Where(x => x.TeamId == game.FirstTeamId || x.TeamId == game.SecondTeamId)
                        .ToList();
                    var player = players[new Random().Next(players.Count)];
                    goal.PlayerId = player.Id;
                    var goalInDb = database.Goals.List
                        .Where(x => x.PlayerId == player.Id && x.GameId == game.Id)
                        .ToList();
                    if (goalInDb.Count == 0)
                    {
                        database.Goals.Append(goal);
                    }
                    else
                    {
                        foreach (var entity in goalInDb)
                        {
                            entity.Count += 1;
                            database.Goals.Update(x => x.Id == entity.Id, entity);
                        }
                    }
                }
            }
        }
    }

    private bool IsNeedToSimulate(GameEntity game, DbContext database)
    {
        var model = GetGameModel(database, game);
        var playedAt = game.PlayedAt;
        var isWas = DateTime.Now >= playedAt;
        var teamsHaveMinPlayers = model.FirstTeam?.Players?.Count != 0 && model.SecondTeam?.Players?.Count != 0;
        var notPayed = database.Goals.List.Where(x => x.GameId == game.Id).ToList().Count == 0;
        return isWas && teamsHaveMinPlayers && notPayed;
    }

    private List<TableItem> CollectTableItems(DbContext database)
    {
        var result = new List<TableItem>();
        var teams = database.Teams.List.ToList();
        foreach (var team in teams)
        {
            var players = database.Players.List.Where(x => x.TeamId == team.Id).ToList();
            var teamGoals = 0;
            foreach (var player in players)
            {
                var goal = database.Goals.List
                    .FirstOrDefault(x => x.PlayerId == player.Id);
                if(goal == null) continue;
                teamGoals += goal.Count;
            }
            result.Add(new TableItem(team, teamGoals));
        }
        return result;
    }
}