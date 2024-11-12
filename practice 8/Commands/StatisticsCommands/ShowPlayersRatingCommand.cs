using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.StatisticsCommands;

public class ShowPlayersRatingCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Players rating.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.WriteLine("| Players full name | Goals |");
        SimulateNotSimulated(database);
        var players = database.Players.List.ToList();
        foreach (var player in players)
        {
            var goals = database.Goals.List
                .Where(x => x.PlayerId == player.Id)
                .Select(x => x.Count)
                .Sum();
            Console.WriteLine($"| {player.Surname} {player.Name} {player.SecondName} | {goals}");
        }
    }
}