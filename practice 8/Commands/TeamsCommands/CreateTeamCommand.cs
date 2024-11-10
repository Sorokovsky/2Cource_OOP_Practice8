using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Commands.TeamsCommands;

public class CreateTeamCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Create team.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var newTeam = TeamEntity.Enter();
        var coach = ChooseDependsOn("coach", database.Coaches);
        newTeam.CoachId = coach.Id;
        database.Teams.Append(newTeam);
    }
}