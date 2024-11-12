using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.PlayersCommands;

public class ShowIfFrom1986Command : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Show players if from 1986.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var found = database.From1986.List.Select(x => new PlayerModel(x)).ToList();
        if(found.Count == 0) Console.WriteLine("No players from 1986.");
        else
        {
            Console.WriteLine("Players from 1986: ");
            var i = 0;
            foreach (var player in found)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(player);
            }
        }
    }
}