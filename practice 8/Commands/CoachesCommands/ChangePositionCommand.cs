using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.CoachesCommands;

public class ChangePositionCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Change position.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a phone number of coach to finding: ");
        var phoneNumber = Console.ReadLine() ?? string.Empty;
        var found = database.Coaches.List
            .Where(x => x.PhoneNumber.Equals(phoneNumber))
            .ToList();
        if(found.Count == 0) Console.WriteLine("No coaches by this phone number.");
        else
        {
            var newPosition = ChooseDependsOn("position", database.Positions);
            foreach (var coach in found)
            {
                coach.PositionId = newPosition.Id;
                database.Coaches.Update(x => coach.Id == x.Id, coach);
            }
        }
    }
}