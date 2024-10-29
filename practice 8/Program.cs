using Practice_8.Commands;
using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Events;

namespace Practice_8;

public static class Program
{
    public static void Main()
    {
        PrepareRoles();
        PrepareEvents();
        var commandContext = PrepareCommands();
        commandContext.Loop();
    }

    private static void PrepareEvents()
    {
        UserEvents.NotLogined += SecurityCenter.UnAuthorized;
    }

    private static CommandContext PrepareCommands()
    {
        CommandContext context = new(new DBContext());
        context.AddCommand(new ExitCommand());
        return context;
    }
        
    private static void PrepareRoles()
    {
        SecurityCenter.Hierarchy.Append(new("Quest", 0));
        SecurityCenter.Hierarchy.Append(new("User", 1));
        SecurityCenter.Hierarchy.Append(new("Admin", 2));
    }
}
