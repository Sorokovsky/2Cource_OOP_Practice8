using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.StadiumsCommands;

public class ShowIfHasGamesCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show stadiums that did have game";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var goals = database.Goals.List.ToList();
        var games = new List<GameEntity>();
        var stadiums = new List<StadiumEntity>();
        foreach (var goal in goals)
        {
            var game = database.Games.List.First(x => x.Id == goal.GameId);
            if (games.FirstOrDefault(x => x.Id == game.Id) == null)
            {
                games.Add(game);
            }
        }

        foreach (var game in games)
        {
            var stadium = database.Stadiums.List.First(x => x.Id == game.StadiumId);
            if (stadiums.FirstOrDefault(x => x.Id == stadium.Id) == null)
            {
                stadiums.Add(stadium);
            }
        }
        if(stadiums.Count == 0) Console.WriteLine("No stadiums that did have games.");
        else
        {
            Console.WriteLine("Stadiums that did have games: ");
            var i = 0;
            foreach (var stadium in stadiums)
            {
                var model = new StadiumModel(stadium, new StadiumTypeModel(database.StadiumTypes.List
                    .First(x => x.Id == stadium.StadiumTypeId)));
                Console.WriteLine($"#{++i}");
                Console.WriteLine(model);
            }
        }
    }
}