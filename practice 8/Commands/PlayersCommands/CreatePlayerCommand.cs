using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Commands.PlayersCommands;

public class CreatePlayerCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Create new player.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var newPlayer = PlayerEntity.Enter();
        var team = ChooseDependsOn("team", database.Teams);
        newPlayer.TeamId = team.Id;
        database.Players.Append(newPlayer);
    }
}