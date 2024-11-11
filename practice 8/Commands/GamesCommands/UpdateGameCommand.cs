using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Commands.GamesCommands;

public class UpdateGameCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Update.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name of game to find: ");
        var name = Console.ReadLine() ?? string.Empty;
        var found = database.Games.List.Where(x => x.Name.Equals(name)).ToList();
        if(found.Count == 0) Console.WriteLine("No games found.");
        else
        {
            var newGame = GameEntity.Enter();
            var stadium = ChooseDependsOn("stadium", database.Stadiums);
            var firstTeam = ChooseDependsOn("first team", database.Teams);
            var secondTeam = ChooseDependsOn("second team", database.Teams);
            while (firstTeam.Id == secondTeam.Id)
            {
                Console.WriteLine("Teams must be different. Try again.");
                firstTeam = ChooseDependsOn("first team", database.Teams);
                secondTeam = ChooseDependsOn("second team", database.Teams);
            }
            newGame.StadiumId = stadium.Id;
            newGame.FirstTeamId = firstTeam.Id;
            newGame.SecondTeamId = secondTeam.Id;
            foreach (var game in found)
            {
                database.Games.Update(x => x.Id == game.Id, newGame);
            }
        }
    }
}