using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.GamesCommands;

public class RemoveGameFrom11September2011Command : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Remover if in 11.9.2011.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        database.Games.Remove(x => DateOnly.FromDateTime(x.PlayedAt) == new DateOnly(2011, 9, 11));
    }
}