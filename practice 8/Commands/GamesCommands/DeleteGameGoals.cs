using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.GamesCommands;

public class DeleteGameGoals : Command 
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Delete games roles.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.WriteLine("Enter a name of game: ");
        var name = Console.ReadLine() ?? string.Empty;
        var games = database.Games.List.Where(x => x.Name.Equals(name));
        foreach (var game in games)
        {
            database.Goals.Remove(x => x.GameId == game.Id);
        }
    }
}