using Practice_8.Commands;
using Practice_8.Database;
using Practice_8.Database.Security;

namespace practice_8.Commands.UserCommands;

public class LoginCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Quest);
    public override string Title { get; set; } = "Login.";
    
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a login: ");
        string login = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a password: ");
        string password = Console.ReadLine() ?? string.Empty;
        SecurityCenter.Login(login, password);
    }
}