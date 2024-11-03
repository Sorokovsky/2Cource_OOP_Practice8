using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Events;

namespace Practice_8.Commands;

public class LogoutCommand : Command
{
    public override UserType NeedUserType { get; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Logout.";

    public LogoutCommand()
    {
        UserEvents.SuccessLogout += OnSuccessLogout;
    }
    
    ~LogoutCommand()
    {
        UserEvents.SuccessLogout -= OnSuccessLogout;
    }
    
    public override void Process(DbContext database)
    {
        SecurityCenter.Logout();
        UserEvents.OnSuccessLogout();
    }

    private void OnSuccessLogout()
    {
        Console.WriteLine("Success logout.");
    }
}