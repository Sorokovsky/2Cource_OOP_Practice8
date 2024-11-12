using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.PlayersCommands;

public class ShowPlayersDescendingBirthdayCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show users with descending birthday";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var found = database.Players.List.ToList()
            .Select(x => new PlayerModel(x)).ToList();
        found.Sort((first, second) => second.Birthday.CompareTo(first.Birthday));
        if (found.Count == 0) Console.WriteLine("No players.");
        else
        {
            Console.WriteLine("Players: ");
            var i = 0;
            foreach (var player in found)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(player);
            }
        }
    }
}