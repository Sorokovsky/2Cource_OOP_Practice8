﻿using Practice_8.Commands;
using Practice_8.Commands.CoachesCommands;
using Practice_8.Commands.GamesCommands;
using Practice_8.Commands.PlayersCommands;
using Practice_8.Commands.PositionsCommands;
using Practice_8.Commands.StadiumsCommands;
using practice_8.Commands.StadiumTypeCommands;
using Practice_8.Commands.StatisticsCommands;
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
            PrepareTeamsCommands(),
            PreparePlayersCommands(),
            PrepareGamesCommands(),
            PrepareStatisticsCommands()
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
            new ShowStadiumByType(),
            new ChangeStadiumPhoneNumberCommand(),
            new ShowIfHasGamesCommand(),
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
            new CreateTeamCommand(),
            new ShowTeamsCommand(),
            new FindTeamCommand(),
            new UpdateTeamCommand(),
            new ChangeCoachCommand(),
            new RemoveTeamCommand()
            );
        return configure.Build();
    }

    private static CommandContext PreparePlayersCommands()
    {
        var configure = new ContextConfigure(DbContext.Singleton());
        configure.Create();
        configure.WithTitle("Players commands.");
        configure.WithCommands(
            new ExitCommand(),
            new CreatePlayerCommand(),
            new ShowPlayersCommand(),
            new FindPlayerCommand(),
            new UpdatePlayerCommand(),
            new ChangeTeamCommand(),
            new ShowPlayersDescendingBirthdayCommand(),
            new ShowPlayersYounger20YearsCommand(),
            new ShowAvarageAgeCommand(),
            new ShowIfFrom1986Command(),
            new ShowSurnameYoungerCommand(),
            new RemovePlayerCommand()
            );
        return configure.Build();
    }

    private static CommandContext PrepareGamesCommands()
    {
        var configure = new ContextConfigure(DbContext.Singleton());
        configure.Create();
        configure.WithTitle("Games menu.");
        configure.WithCommands(
            new ExitCommand(),
            new CreateGameCommand(),
            new ShowGamesCommand(),
            new FindGameCommand(),
            new UpdateGameCommand(),
            new ChangeStadiumCommand(),
            new ChangeFirstTeamCommand(),
            new ChangeSecondTeamCommand(),
            new ShowGamesInJulyCommand(),
            new ShowGamesIn2012AugustCommand(),
            new ShowDateFirstGameCommand(),
            new RemoveGameCommand(),
            new RemoveGameFrom11September2011Command(),
            new DeleteGameGoals()
            );
        return configure.Build();
    }

    private static CommandContext PrepareStatisticsCommands()
    {
        var configure = new ContextConfigure(DbContext.Singleton());
        configure.Create();
        configure.WithTitle("Statistics menu.");
        configure.WithCommands(
            new ExitCommand(),
            new ShowTournerCommand(),
            new ShowPlayersRatingCommand()
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