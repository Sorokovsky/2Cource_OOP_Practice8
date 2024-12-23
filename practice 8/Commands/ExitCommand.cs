using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands;

public class ExitCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Quest);
    public override string Title { get; set; } = "Exit.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        currentContext.Exit();
    }
}