using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.PlayersCommands;

public class ShowPlayersCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show all.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var found = database.Players.List
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
        if(found.Count == 0) Console.WriteLine("No players.");
        else
        {
            var i = 0;
            Console.WriteLine("Players: ");
            foreach (var player in found)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(player);
            }
        }
    }
}