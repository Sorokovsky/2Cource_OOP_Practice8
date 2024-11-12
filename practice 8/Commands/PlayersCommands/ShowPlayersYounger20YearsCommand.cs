using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.PlayersCommands;

public class ShowPlayersYounger20YearsCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show players younger 20 years.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var found = database.Players.List
            .Where(x =>
            {
                var age = Math.Abs(DateTime.Now.Year - x.Birthday.Year);
                return age < 20;
            }).ToList();
        if(found.Count == 0) Console.WriteLine("No players.");
        else
        {
            Console.WriteLine("Players younger 20 years: ");
            var i = 0;
            foreach (var player in found)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(player);
            }
        }
    }
}