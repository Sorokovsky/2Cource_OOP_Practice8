using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Commands.CoachesCommands;

public class UpdateCoachCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Update.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a phone number of coach for finding: ");
        var phoneNumber = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Enter a new date");
        var newCoach = CoachEntity.Enter();
        var position = ChooseDependsOn("position", database.Positions);
        newCoach.PositionId = position.Id;
        database.Coaches.Update(x => x.PhoneNumber.Equals(phoneNumber), newCoach);
    }
}