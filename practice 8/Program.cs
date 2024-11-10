using Practice_8.Commands;
using Practice_8.Commands.CoachesCommands;
using Practice_8.Commands.PositionsCommands;
using Practice_8.Commands.StadiumsCommands;
using practice_8.Commands.StadiumTypeCommands;
using Practice_8.Commands.TeamsCommands;
using practice_8.Commands.UserCommands;
using Practice_8.Database;
using Practice_8.Database.Entities;
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
        UserEvents.NotLoginned += UnAuthorized;
        EntitySuccessEvents.Created += OnCreated;
        UserEvents.InvalidLogin += OnInvalidLogin;
        UserEvents.InvalidPassword += OnInvalidPassword;
        UserEvents.SuccessLoginned += OnSuccessLoginned;
        UserEvents.SuccessLogout += OnSuccessLogout;
        EntitySuccessEvents.Updated += OnUpdated;
        EntitySuccessEvents.Removed += OnRemoved;
        EntityFailedEvents.NotDeleted += OnNotDeleted;
    }

    private static CommandContext PrepareMainCommands()
    {
        var mainConfigure = new ContextConfigure(DbContext.Singleton());
        mainConfigure.Create();
        mainConfigure.WithTitle("Main menu.");
        mainConfigure.WithCommands(
            new ExitCommand(), 
            PrepareUserContext(), 
            PrepareStadiumTypesContext(),
            PrepareStadiumsCommands(),
            PreparePositionsCommands(),
            PrepareCoachesCommands(),
            PrepareTeamsCommands()
            );
        return mainConfigure.Build();
    }

    private static CommandContext PrepareUserContext()
    {
        var userConfigure = new ContextConfigure(DbContext.Singleton());
        userConfigure.Create();
        userConfigure.WithTitle("Users menu.");
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
        configure.WithCommands(
            new ExitCommand(),
            new CreateStadiumTypeCommand(),
            new UpdateStadiumTypeCommand(),
            new ShowStadiumTypesCommand(),
            new FindStadiumTypesCommand(),
            new RemoveStadiumTypeCommand()
            );
        return configure.Build();
    }

    private static CommandContext PrepareStadiumsCommands()
    {
        var configure = new ContextConfigure(DbContext.Singleton());
        configure.Create();
        configure.WithTitle("Stadiums menu.");
        configure.WithCommands(
            new ExitCommand(), 
            new CreateStadiumCommand(),
            new ShowStadiumsCommand(),
            new FindStadiumsCommand(),
            new FindByTypeCommand(),
            new UpdateStadiumCommand(),
            new ChangeTypeCommand(),
            new RemoveStadiumCommand()
            );
        return configure.Build();
    }

    private static CommandContext PreparePositionsCommands()
    {
        var configure = new ContextConfigure(DbContext.Singleton());
        configure.Create();
        configure.WithTitle("Positions menu.");
        configure.WithCommands(
            new ExitCommand(),
            new CreatePositionCommand(),
            new ShowPositionsCommand(),
            new FindByPositionNameCommand(),
            new UpdatePositionCommand(),
            new RemovePositionCommand()
            );
        return configure.Build();
    }

    private static CommandContext PrepareCoachesCommands()
    {
        var configure = new ContextConfigure(DbContext.Singleton());
        configure.Create();
        configure.WithTitle("Coaches menu.");
        configure.WithCommands(
            new ExitCommand(),
            new CreateCoachCommand(),
            new ShowCoachesCommand(),
            new FindCoachesCommand(),
            new UpdateCoachCommand(),
            new ChangePositionCommand(),
            new RemoveCoachCommand()
            );
        return configure.Build();
    }

    private static CommandContext PrepareTeamsCommands()
    {
        var configure = new ContextConfigure(DbContext.Singleton());
        configure.Create();
        configure.WithTitle("Teams menu.");
        configure.WithCommands(
            new ExitCommand(),
            new CreateTeamCommand()
            );
        return configure.Build();
    }

    private static void OnCreated(BaseEntity entity)
    {
        Console.WriteLine($"{entity.GetType().Name.Replace("Entity", "")} successfully created.");
    }
    
    private static void OnInvalidLogin()
    {
        Console.WriteLine("Invalid login. Try again.");
    }

    private static void OnInvalidPassword()
    {
        Console.WriteLine("Invalid password. Try again.");
    }

    private static void OnSuccessLoginned()
    {
        Console.WriteLine("Success loginned.");
    }
    
    private static void OnSuccessLogout()
    {
        Console.WriteLine("Success logout.");
    }
    
    private static void UnAuthorized()
    {
        Console.WriteLine("You are not authorized to log out. Please authorize.");
    }

    private static void OnUpdated(BaseEntity entity)
    {
        Console.WriteLine($"{entity.GetType().Name.Replace("Entity", "")} was updated where id == {entity.Id}");
    }

    private static void OnRemoved(BaseEntity entity)
    {
        Console.WriteLine($"{entity.GetType().Name.Replace("Entity", "")} was removed where id == {entity.Id}");
    }

    private static void OnNotDeleted(BaseEntity entity, string reason)
    {
        Console.WriteLine($"{entity.GetType().Name.Replace("Entity", "")} not deleted. {reason}");
    }
}