using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.GamesCommands;

public class ShowGamesIn2012AugustCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show games in 2012 August";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var found = database.Games.List
            .Where(x => x.PlayedAt.Year == 2012 && x.PlayedAt.Month == 8)
            .Select(x => GetGameModel(database, x))
            .ToList();
        if(found.Count == 0) Console.WriteLine("No games in 2012 August.");
        else
        {
            Console.WriteLine("Games in 2012 August.");
            var i = 0;
            foreach (var game in found)
            {
                Console.WriteLine($"{++i}");
                Console.WriteLine(game);
            }
        }
    }
}