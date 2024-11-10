using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.TeamsCommands;

public class FindTeamCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Find by name";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name of team for found: ");
        var name = Console.ReadLine() ?? string.Empty;
        var found = database.Teams.List
            .Where(x => x.Name.Equals(name))
            .Join(
                database.Coaches.List,
                entity => entity.CoachId,
                entity => entity.Id,
                (entity, coachEntity) => new TeamModel(
                    entity,
                    new CoachModel(coachEntity, new PositionModel(database.Positions.List.First(x => x.Id == coachEntity.PositionId))),
                    database.Players.List
                        .Where(x => x.TeamId == entity.Id)
                        .Select(x => new PlayerModel(x))
                        .ToList()
                    ) 
            ).ToList();
        if(found.Count == 0) Console.WriteLine("No teams found by name.");
        else
        {
            Console.WriteLine($"Teams by name == {name}");
            var i = 0;
            foreach (var team in found)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(team);
            }
        }
    }
}