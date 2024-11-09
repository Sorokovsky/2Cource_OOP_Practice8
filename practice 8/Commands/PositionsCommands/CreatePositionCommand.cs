using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Commands.PositionsCommands;

public class CreatePositionCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Create a position.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        database.Positions.Append(PositionEntity.Enter());
    }
}