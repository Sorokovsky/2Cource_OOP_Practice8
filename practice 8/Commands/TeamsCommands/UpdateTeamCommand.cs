using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Commands.TeamsCommands;

public class UpdateTeamCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Update team.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name for finding: ");
        var name = Console.ReadLine() ?? string.Empty;
        var found = database.Teams.List.Where(x => x.Name.Equals(name)).ToList();
        if(found.Count == 0) Console.WriteLine("Not found teams for updating.");
        else
        {
            var newTeam = TeamEntity.Enter();
            newTeam.CoachId = ChooseDependsOn("coach", database.Coaches).Id;
            foreach (var team in found)
            {
                database.Teams.Update(x => x.Id == team.Id, newTeam);
            }
        }
    }
}