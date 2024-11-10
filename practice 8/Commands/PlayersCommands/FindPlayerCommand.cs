using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.PlayersCommands;

public class FindPlayerCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Find by full name.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a surname of player: ");
        var surname = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a name of player: ");
        var name = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a last name of player: ");
        var lastname = Console.ReadLine() ?? string.Empty;
        var found = database.Players.List
            .Where(x => x.Surname.Equals(surname) && x.Name.Equals(name) && x.SecondName.Equals(lastname))
            .Join(
                database.Teams.List,
                entity => entity.TeamId,
                entity => entity.Id,
                (entity, teamEntity) => new PlayerModel(
                    entity,
                    new TeamModel(
                        teamEntity,
                        new CoachModel(
                            database.Coaches.List.First(x => x.Id == teamEntity.CoachId),
                            new PositionModel(database.Positions.List.First(x =>
                                x.Id == database.Coaches.List.First(x => x.Id == teamEntity.CoachId).PositionId))
                        )
                    )
                )
            ).ToList();
        if(found.Count == 0) Console.WriteLine("No players found.");
        else
        {
            Console.WriteLine("Found players: ");
            var i = 0;
            foreach (var player in found)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(player);
            }
        }
    }
}