using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.TeamsCommands;

public class ChangeCoachCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Change coach of team";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name of team for finding: ");
        var name = Console.ReadLine() ?? string.Empty;
        var found = database.Teams.List.Where(x => x.Name.Equals(name)).ToList();
        if(found.Count == 0) Console.WriteLine("No teams to change coach.");
        else
        {
            var newCoach = ChooseDependsOn("coach", database.Coaches);
            foreach (var team in found)
            {
                team.CoachId = newCoach.Id;
                database.Teams.Update(x => x.Id == team.Id, team);
            }
        }
    }
}