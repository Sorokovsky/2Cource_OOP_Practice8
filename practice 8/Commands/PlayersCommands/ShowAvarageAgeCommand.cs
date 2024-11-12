using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.PlayersCommands;

public class ShowAvarageAgeCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Get avarage age.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var avarage = database.Players.List
            .Select(x => Math.Abs(DateTime.Now.Year - x.Birthday.Year)).Average();
        Console.WriteLine($"Avarage age of players: {avarage}.");
    }
}