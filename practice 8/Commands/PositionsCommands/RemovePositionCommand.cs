using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.PositionsCommands;

public class RemovePositionCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Remove by name.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name for found: ");
        var name = Console.ReadLine() ?? string.Empty;
        database.Positions.Remove(x => x.Name.Equals(name));
    }
}