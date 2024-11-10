using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.PlayersCommands;

public class RemovePlayerCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Remove by full name.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a surname of player: ");
        var surname = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a name of player: ");
        var name = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a last name of player: ");
        var lastname = Console.ReadLine() ?? string.Empty;
        database.Players.Remove(x => x.Surname.Equals(surname) && x.Name.Equals(name) && x.SecondName.Equals(lastname));
    }
}