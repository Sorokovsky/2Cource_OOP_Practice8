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
        items.Sort((first, second) => second.Mark.CompareTo(first.Mark));
        Console.WriteLine("|   Team  |  Score  |");
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
        var games = database.Games.List.ToList();
        foreach (var game in games)
        {
            var firstGoals = 0;
            var secondGoals = 0;
            var firstTeam = database.Teams.List.First(x => x.Id == game.FirstTeamId);
            var firstPlayers = database.Players.List
                .Where(x => x.TeamId == firstTeam.Id)
                .ToList();
            var secondTeam = database.Teams.List.First(x => x.Id == game.SecondTeamId);
            var secondPlayers = database.Players.List
                .Where(x => x.TeamId == secondTeam.Id)
                .ToList();
            foreach (var player in firstPlayers)
            {
                var goal = database.Goals.List.FirstOrDefault(x => x.PlayerId == player.Id && x.GameId == game.Id);
                if(goal == null) continue;
                firstGoals += goal.Count;
            }
            foreach (var player in secondPlayers)
            {
                var goal = database.Goals.List.FirstOrDefault(x => x.PlayerId == player.Id && x.GameId == game.Id);
                if(goal == null) continue;
                secondGoals += goal.Count;
            }
            var foundFirst = result.Where(x => x.Team.Id == firstTeam.Id).FirstOrDefault();
            var foundSecond = result.Where(x => x.Team.Id == secondTeam.Id).FirstOrDefault();
            var firstScore = 0;
            var secondScore = 0;
            if (firstGoals > secondGoals) firstScore = 3;
            if (firstGoals == secondGoals)
            {
                firstScore = 1;
                secondScore = 1;
            }
            if (firstGoals < secondGoals) secondScore = 3;
            if (foundFirst == null)
            {
                result.Add(new TableItem(firstTeam, firstScore));
            }
            else
            {
                foundFirst.Mark += firstScore;
            }

            if (foundSecond == null)
            {
                result.Add(new TableItem(secondTeam, secondScore));
            }
            else
            {
                foundSecond.Mark += secondScore;
            }
        }
        return result;
    }
}