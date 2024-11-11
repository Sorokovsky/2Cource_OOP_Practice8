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
    
    private TeamModel GetTeamModel(DbContext database, int teamId)
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
}