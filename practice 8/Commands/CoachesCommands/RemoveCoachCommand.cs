using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.CoachesCommands;

public class RemoveCoachCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Remove.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a phone number of coach to finding: ");
        var phoneNumber = Console.ReadLine() ?? string.Empty;
        database.Coaches.Remove(x => x.PhoneNumber.Equals(phoneNumber));
    }
}