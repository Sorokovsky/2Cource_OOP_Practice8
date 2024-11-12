using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.GamesCommands;

public class ShowGamesInJulyCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show games in July.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var found = database.Games.List
            .Where(x => x.PlayedAt.Month == 7)
            .Select(x => GetGameModel(database, x))
            .ToList();
        if(found.Count == 0) Console.WriteLine("No games in July.");
        else
        {
            Console.WriteLine("Games in July: ");
            var i = 0;
            foreach (var game in found)
            {
                Console.WriteLine($"{++i}");
                Console.WriteLine(game);
            }
        }
    }
}