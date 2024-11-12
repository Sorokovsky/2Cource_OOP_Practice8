using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.GamesCommands;

public class ShowDateFirstGameCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show date of first game.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var games = database.Games.List
            .Select(x => GetGameModel(database, x))
            .ToList();
        if(games.Count == 0) Console.WriteLine("No games.");
        else
        {
            games
                .Sort((first, second) => first.PlayedAtDate.CompareTo(second.PlayedAtDate));
            var firstDate = games.First().PlayedAtDate;
            Console.WriteLine($"Date of a first game: {firstDate.Day}.{firstDate.Month}.{firstDate.Year}.");
        }
    }
}