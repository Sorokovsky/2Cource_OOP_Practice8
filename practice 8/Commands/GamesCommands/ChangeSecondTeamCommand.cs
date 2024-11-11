using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.GamesCommands;

public class ChangeSecondTeamCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Change second team.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name of game: ");
        var name = Console.ReadLine() ?? string.Empty;
        var found = database.Games.List.Where(x => x.Name.Equals(name)).ToList();
        if(found.Count == 0) Console.WriteLine("No game found.");
        else
        {
            var secondTeam = ChooseDependsOn("second team", database.Teams);
            foreach (var game in found)
            {
                while (game.FirstTeamId == secondTeam.Id)
                {
                    Console.WriteLine($"In game where id == {game.Id}. First game equals second. choose another second team.");
                    secondTeam = ChooseDependsOn("second team", database.Teams);
                }
                game.SecondTeamId = secondTeam.Id;
                database.Games.Update(x => x.Id == game.Id, game);
            }
        }
    }
}