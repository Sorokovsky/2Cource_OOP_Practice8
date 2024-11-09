using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.PositionsCommands;

public class ShowPositionsCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show all.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        if(database.Positions.List.Count == 0) Console.WriteLine("No positions found.");
        else
        {
            Console.WriteLine("Positions: ");
            var i = 0;
            foreach (var position in database.Positions.List.Select(x => new PositionModel(x)))
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(position);
            }
        }
    }
}