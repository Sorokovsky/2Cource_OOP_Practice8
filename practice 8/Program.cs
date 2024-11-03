using Practice_8.Commands;
using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Events;

namespace Practice_8;

public static class Program
{
    public static void Main()
    {
        AppContext.SetSwitch("System.Runtime.Serialization.Formatters.Binary.BinaryFormatter.EnableUnsafeDeserialization", true);
        SecurityCenter.PrepareRoles();
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
        context.AddCommand(new RegisterCommand());
        context.AddCommand(new LoginCommand());
        return context;
    }
}
