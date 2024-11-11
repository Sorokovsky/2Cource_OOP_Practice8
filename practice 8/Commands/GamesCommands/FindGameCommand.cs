using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.GamesCommands;

public class FindGameCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Find by name.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name of game: ");
        var name = Console.ReadLine() ?? string.Empty;
        var found = database.Games.List
            .Where(x => x.Name.Equals(name))
            .Select(x => GetGameModel(database, x))
            .ToList();
        if(found.Count == 0) Console.WriteLine("No one game found.");
        else
        {
            Console.WriteLine("Found games: ");
            var i = 0;
            foreach (var game in found)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(game);
            }
        }
    }
}