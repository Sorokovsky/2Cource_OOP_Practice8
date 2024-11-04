using Practice_8.Commands;
using practice_8.Commands.StadiumTypeCommands;
using practice_8.Commands.UserCommands;
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
        UserEvents.NotLoginned += SecurityCenter.UnAuthorized;
    }

    private static CommandContext PrepareCommands()
    {
        var database = new DbContext();
        CommandContext context = new(database, "Main menu", UserType.Create(Roles.Quest));
        context.AddCommand(new ExitCommand());
        var usersContext = new CommandContext(database, "Users menu", UserType.Create(Roles.Quest));
        usersContext.AddCommand(new ExitCommand());
        usersContext.AddCommand(new RegisterCommand());
        usersContext.AddCommand(new LoginCommand());
        usersContext.AddCommand(new ShowAccountCommand());
        usersContext.AddCommand(new LogoutCommand());
        usersContext.AddCommand(new CreateStadiumTypeCommand());
        context.AddCommand(usersContext);
        return context;
    }
}
