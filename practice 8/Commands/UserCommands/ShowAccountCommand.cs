using Practice_8.Commands;
using Practice_8.Database;
using Practice_8.Database.Security;

namespace practice_8.Commands.UserCommands;

public class ShowAccountCommand : Command
{
    public override UserType NeedUserType { get; } = UserType.Create(Roles.Quest);
    public override string Title { get; set; } = "Show account.";
    public override void Process(DbContext database)
    {
        Console.WriteLine("Your account.");
        Console.WriteLine(SecurityCenter.CurrentUser);
    }
}