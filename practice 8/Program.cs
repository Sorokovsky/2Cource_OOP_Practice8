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
        var commandContext = PrepareMainCommands();
        commandContext.Loop();
    }

    private static void PrepareEvents()
    {
        UserEvents.NotLoginned += SecurityCenter.UnAuthorized;
    }

    private static CommandContext PrepareMainCommands()
    {
        var mainConfigure = new ContextConfigure(DbContext.Singleton());
        mainConfigure.Create();
        mainConfigure.WithTitle("Main menu.");
        mainConfigure.WithRole(UserType.Create(Roles.Quest));
        mainConfigure.WithCommands(new ExitCommand(), PrepareUserContext(), PrepareStadiumTypesContext());
        return mainConfigure.Build();
    }

    private static CommandContext PrepareUserContext()
    {
        var userConfigure = new ContextConfigure(DbContext.Singleton());
        userConfigure.Create();
        userConfigure.WithTitle("Users menu.");
        userConfigure.WithRole(UserType.Create(Roles.Quest));
        userConfigure.WithCommands(
            new ExitCommand(), 
            new RegisterCommand(), 
            new LoginCommand(), 
            new ShowAccountCommand(),
            new LogoutCommand()
            );
        return userConfigure.Build();
    }

    private static CommandContext PrepareStadiumTypesContext()
    {
        var configure = new ContextConfigure(DbContext.Singleton());
        configure.Create();
        configure.WithTitle("Stadium types menu.");
        configure.WithRole(UserType.Create(Roles.User));
        configure.WithCommands(
            new ExitCommand(),
            new CreateStadiumTypeCommand(),
            new ShowStadiumTypesCommand()
            );
        return configure.Build();
    }
}
