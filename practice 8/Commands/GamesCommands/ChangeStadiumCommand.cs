using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.GamesCommands;

public class ChangeStadiumCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Change a stadium.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name of game: ");
        var name = Console.ReadLine() ?? string.Empty;
        var found = database.Games.List.Where(x => x.Name.Equals(name)).ToList();
        if(found.Count == 0) Console.WriteLine("No games found.");
        else
        {
            var newStadium = ChooseDependsOn("stadium", database.Stadiums);
            foreach (var game in found)
            {
                game.StadiumId = newStadium.Id;
                database.Games.Update(x => x.Id == game.Id, game);
            }
        }
    }
}