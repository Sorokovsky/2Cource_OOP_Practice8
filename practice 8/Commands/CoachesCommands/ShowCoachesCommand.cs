using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.CoachesCommands;

public class ShowCoachesCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show all.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var coaches = database.Coaches.List
            .Join(
                database.Positions.List,
                entity => entity.PositionId,
                entity => entity.Id,
                (entity, positionEntity) => new CoachModel(entity, new PositionModel(positionEntity))
            ).ToList();
        if(coaches.Count == 0) Console.WriteLine("No coaches");
        else
        {
            Console.WriteLine("Coaches: ");
            var i = 0;
            foreach (var coach in coaches)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(coach);
            }
        }
    }
}