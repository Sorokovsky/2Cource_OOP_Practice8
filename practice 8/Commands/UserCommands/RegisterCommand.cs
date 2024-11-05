using Practice_8.Commands;
using Practice_8.Database;
using Practice_8.Database.Security;

namespace practice_8.Commands.UserCommands;

public class RegisterCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Quest);
    public override string Title { get; set; } = "Register account";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a login: "); var username = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a password: "); var password = Console.ReadLine() ?? string.Empty;
        var user = new User(username, password, ChooseRole());
        database.Users.Append(user);
    }

    private UserType ChooseRole()
    {
        Console.WriteLine("Choose a role.");
        int i = 0;
        foreach (var role in SecurityCenter.Hierarchy.Roles)
        {
            i++;
            Console.WriteLine($"{i}-{role}");
        }

        Console.WriteLine(">> ");
        int choise = Convert.ToInt32(Console.ReadLine());
        while (choise < 1 || choise > SecurityCenter.Hierarchy.Roles.Count)
        {
            Console.Write("Invalid choise. Try again: ");
            choise = Convert.ToInt32(Console.ReadLine());
        }
        return SecurityCenter.Hierarchy.Roles.ElementAt(i - 1);
    }
}