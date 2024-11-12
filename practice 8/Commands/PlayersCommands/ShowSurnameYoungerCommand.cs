using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.PlayersCommands;

public class ShowSurnameYoungerCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show surname fo younger player.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var found = database.Players.List.ToList();
        found.Sort((first, second) => second.Birthday.CompareTo(first.Birthday));
        var player = found.FirstOrDefault();
        if(player == null) Console.WriteLine("No one player.");
        else
        {
            Console.WriteLine($"Surname of younger player: {player.Surname}.");
        }
    }
}