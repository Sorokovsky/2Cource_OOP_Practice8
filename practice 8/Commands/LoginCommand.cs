using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Events;

namespace Practice_8.Commands;

public class LoginCommand : Command
{
    public override UserType NeedUserType { get; } = UserType.Create(Roles.Quest);
    public override string Title { get; set; } = "Login.";

    public LoginCommand()
    {
        UserEvents.InvalidLogin += OnInvalidLogin;
        UserEvents.InvalidPassword += OnInvalidPassword;
        UserEvents.SuccessLoginned += OnSuccessLoginned;
    }

    ~LoginCommand()
    {
        UserEvents.InvalidLogin -= OnInvalidLogin;
        UserEvents.InvalidPassword -= OnInvalidPassword;
        UserEvents.SuccessLoginned -= OnSuccessLoginned;
    }
    
    public override void Process(DBContext database)
    {
        Console.Write("Enter a login: ");
        string login = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a password: ");
        string password = Console.ReadLine() ?? string.Empty;
        SecurityCenter.Login(login, password);
    }

    private void OnInvalidLogin()
    {
        Console.WriteLine("Invalid login. Try again.");
    }

    private void OnInvalidPassword()
    {
        Console.WriteLine("Invalid password. Try again.");
    }

    private void OnSuccessLoginned()
    {
        Console.WriteLine("Success loginned.");
    }
}