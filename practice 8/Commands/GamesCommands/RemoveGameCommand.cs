using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.GamesCommands;

public class RemoveGameCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Remove.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name of game: ");
        var name = Console.ReadLine() ?? string.Empty;
        database.Games.Remove(x => x.Name.Equals(name));
    }
}