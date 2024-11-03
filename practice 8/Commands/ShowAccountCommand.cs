using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands;

public class ShowAccountCommand : Command
{
    public override UserType NeedUserType { get; } = UserType.Create(Roles.Quest);
    public override string Title { get; set; } = "Show account.";
    public override void Process(DBContext database)
    {
        Console.WriteLine("Your account.");
        Console.WriteLine(SecurityCenter.CurrentUser);
    }
}