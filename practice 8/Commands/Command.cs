using System.Reflection;
using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;
using Practice_8.Models;
using Index = Practice_8.Database.IndexSystem.Index;

namespace Practice_8.Commands;

public abstract class Command
{
    protected static int CurrentNumber;
    
    public abstract UserType NeedUserType { get; set; }

    public int Number { get; set; } = CurrentNumber++;

    public abstract string Title { get; set; }

    public abstract void Process(DbContext database, CommandContext currentContext);

    protected virtual T ChooseDependsOn<T>(string title, Repository<T> repository, bool canCreate = true) where T : BaseEntity
    {
        Console.WriteLine($"Choose a {title} type.");
        var i = 0;
        foreach (var type in repository.List)
        {
            Console.WriteLine($"Index: {i++}");
            Console.WriteLine(type);
        }

        var canCreateText = canCreate ? " or -1 to create new" : string.Empty;
        Console.Write($"Enter a index{canCreateText}: "); var index = Convert.ToInt32(Console.ReadLine());
        while (index < (canCreate ? -1 : 0) || index >= repository.Count)
        {
            Console.WriteLine("Invalid index. Try again: ");
            index = Convert.ToInt32(Console.ReadLine());
        }

        if (index == -1 && canCreate)
        {
            var type = Type.GetType(repository.GetType().GenericTypeArguments.First().FullName ?? typeof(BaseEntity).FullName) ?? typeof(BaseEntity);
            var instance = Activator.CreateInstance(type);
            if (instance != null)
            {
                var newItem = (T)instance.GetType().GetMethod("Enter").Invoke(instance, null);
                List<Index> dependsOn = DbContext.Singleton().GetDependenceOnTypes(newItem);
                if (dependsOn.Count != 0)
                {
                    foreach (var dependOn in dependsOn)
                    {
                        var name = dependOn.DependsOnType.Name.Replace("Entity", "").ToLower();
                        var repos = typeof(DbContext).GetProperties();
                        var repo = (dynamic)repos
                            .First(x => x.PropertyType.GenericTypeArguments.First().Name.Equals(dependOn.DependsOnType.Name))
                            .GetValue(DbContext.Singleton());
                        var dependenceOn = (BaseEntity)ChooseDependsOn(name, repo);
                        newItem.GetType().GetProperties()
                            .First(x => x.Name.Equals(dependOn.FieldName))
                            .SetValue(newItem, dependenceOn.Id);
                    }
                }
                return repository.Append(newItem);
            }
        }
        return repository[index];
    }

    protected GameModel GetGameModel(DbContext database, GameEntity game)
    {
        var stadiumEntity = database.Stadiums.List.First(x => x.Id == game.StadiumId);
        var stadiumType = database.StadiumTypes.List.First(x => x.Id == stadiumEntity.StadiumTypeId);
        var firstTeam = GetTeamModel(database, game.FirstTeamId);
        var secondTeam = GetTeamModel(database, game.SecondTeamId);
        return new GameModel(game, firstTeam, secondTeam,
            new StadiumModel(stadiumEntity, new StadiumTypeModel(stadiumType)));
    }
    
    protected TeamModel GetTeamModel(DbContext database, int teamId)
    {
        var team = database.Teams.List.First(x => x.Id == teamId);
        var coachEntity = database.Coaches.List.First(x => x.Id == team.CoachId);
        var position = database.Positions.List.First(x => x.Id == coachEntity.PositionId);
        var players = database.Players.List
            .Where(x => x.TeamId == teamId)
            .Select(x => new PlayerModel(x))
            .ToList();
        return new TeamModel(team, new CoachModel(coachEntity, new PositionModel(position)), players);
    }
    
    protected void SimulateNotSimulated(DbContext database)
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

    protected bool IsNeedToSimulate(GameEntity game, DbContext database)
    {
        var model = GetGameModel(database, game);
        var playedAt = game.PlayedAt;
        var isWas = DateTime.Now >= playedAt;
        var teamsHaveMinPlayers = model.FirstTeam?.Players?.Count != 0 && model.SecondTeam?.Players?.Count != 0;
        var notPayed = database.Goals.List.Where(x => x.GameId == game.Id).ToList().Count == 0;
        return isWas && teamsHaveMinPlayers && notPayed;
    }

    protected List<TableItem> CollectTableItems(DbContext database)
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