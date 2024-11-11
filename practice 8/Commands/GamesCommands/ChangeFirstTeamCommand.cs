using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.GamesCommands;

public class ChangeFirstTeamCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Change first team.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name of game: ");
        var name = Console.ReadLine() ?? string.Empty;
        var found = database.Games.List.Where(x => x.Name.Equals(name)).ToList();
        if(found.Count == 0) Console.WriteLine("No games found.");
        else
        {
            var newFirstTeam = ChooseDependsOn("first team", database.Teams);
            foreach (var game in found)
            {
                while (game.SecondTeamId == newFirstTeam.Id)
                {
                    Console.WriteLine($"For game name == {name}. Not different team. Choose another first team.");
                    newFirstTeam = ChooseDependsOn("first team", database.Teams);
                }
                game.FirstTeamId = newFirstTeam.Id;
                database.Games.Update(x => x.Id == game.Id, game);
            }
        }
    }
}