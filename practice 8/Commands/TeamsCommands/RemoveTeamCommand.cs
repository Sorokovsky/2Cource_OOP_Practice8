using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.TeamsCommands;

public class RemoveTeamCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Remove by name.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name for deleting: ");
        var name = Console.ReadLine() ?? string.Empty;
        database.Teams.Remove(x => x.Name.Equals(name));
    }
}