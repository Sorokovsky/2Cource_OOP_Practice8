using Practice_8.Commands;
using Practice_8.Database;
using Practice_8.Database.Security;

namespace practice_8.Commands.StadiumTypeCommands;

public class FindStadiumTypeCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Find by name.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name for founding: " );
        var name = Console.ReadLine() ?? string.Empty;
        var found = database.StadiumTypes.List.Where(x => x.Name.Equals(name)).ToList();
        if(found.Count == 0) Console.WriteLine("No one stadium type found.");
        else
        {
            Console.WriteLine("Found");
            var i = 0;
            foreach (var type in found)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(type);
            }
        }
    }
}