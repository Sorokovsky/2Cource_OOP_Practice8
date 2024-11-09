using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Commands.PositionsCommands;

public class UpdatePositionCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Update position.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name of position for finding: ");
        var name = Console.ReadLine() ?? string.Empty;
        var newPosition = PositionEntity.Enter();
        database.Positions.Update(x => x.Name.Equals(name), newPosition);
    }
}