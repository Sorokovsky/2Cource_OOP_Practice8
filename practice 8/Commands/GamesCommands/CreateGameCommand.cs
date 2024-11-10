using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Commands.GamesCommands;

public class CreateGameCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Create a game.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var newGame = GameEntity.Enter();
        var stadium = ChooseDependsOn("stadium", database.Stadiums);
        var firstTeam = ChooseDependsOn("first team", database.Teams);
        var secondTeam = ChooseDependsOn("second team", database.Teams);
        while (firstTeam.Id == secondTeam.Id)
        {
            Console.WriteLine("Teams must be a different. Try again");
            firstTeam = ChooseDependsOn("first team", database.Teams);
            secondTeam = ChooseDependsOn("second team", database.Teams);
        }
        newGame.StadiumId = stadium.Id;
        newGame.FirstTeamId = firstTeam.Id;
        newGame.SecondTeamId = secondTeam.Id;
        database.Games.Append(newGame);
    }
}