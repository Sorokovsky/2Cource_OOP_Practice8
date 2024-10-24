using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands;

public class ExitCommand : Command
{
    public override string NeedUserType { get; } = UserType.User;
    public override string Title { get; set; } = "Exit.";
    public override void Process(DBContext database)
    {
        Environment.Exit(0);
    }
}