using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Commands.CoachesCommands;

public class CreateCoachCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Create a coach";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var newCoach = CoachEntity.Enter();
        var position = ChooseDependsOn("position", database.Positions);
        newCoach.PositionId = position.Id;
        database.Coaches.Append(newCoach);
    }
}