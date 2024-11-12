using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.StatisticsCommands;

public class ShowTournerCommand : Command 
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show tournier table.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        SimulateNotSimulated(database);
        var items = CollectTableItems(database);
        items.Sort((first, second) => second.Mark.CompareTo(first.Mark));
        Console.WriteLine("|   Team  |  Score  |");
        foreach (var item in items)
        {
            Console.WriteLine($"|{item.Team.Name}|{item.Mark}|");
        }
    }
    
}