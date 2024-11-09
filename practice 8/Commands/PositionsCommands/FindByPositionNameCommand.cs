using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.PositionsCommands;

public class FindByPositionNameCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Find by name.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name of position for finding: ");
        var name = Console.ReadLine() ?? string.Empty;
        var found = database.Positions.List
            .Where(x => x.Name.Equals(name))
            .Select(x => new PositionModel(x))
            .ToList();
        if(found.Count == 0) Console.WriteLine($"Not found positions by name == {name}");
        else
        {
            Console.WriteLine("Positions: ");
            var i = 0;
            foreach (var position in found)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(position);
            }
        }
    }
}