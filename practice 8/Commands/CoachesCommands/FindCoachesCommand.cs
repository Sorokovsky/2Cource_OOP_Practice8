using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.CoachesCommands;

public class FindCoachesCommand : Command 
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Find by phone number.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a phone number of coaches for found: ");
        var phoneNumber = Console.ReadLine() ?? string.Empty;
        var found = database.Coaches.List
            .Where(x => x.PhoneNumber.Equals(phoneNumber))
            .Join(
                database.Positions.List,
                entity => entity.PositionId,
                entity => entity.Id,
                (entity, positionEntity) => new CoachModel(entity, new PositionModel(positionEntity)) 
            ).ToList();
        if(found.Count == 0) Console.WriteLine("No coaches by this phone number.");
        else
        {
            Console.WriteLine("Coaches by phone number: ");
            var i = 0;
            foreach (var coach in found)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(coach);
            }
        }
    }
}