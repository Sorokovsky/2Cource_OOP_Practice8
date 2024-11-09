using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.StadiumsCommands;

public class FindByTypeCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Find by stadium type.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var type = ChooseDependsOn("stadium type", database.StadiumTypes, false);
        var found = database.Stadiums.List
            .Where(x => x.StadiumTypeId == type.Id)
            .Select(x => new StadiumModel(x, new StadiumTypeModel(type)))
            .ToList();
        if(found.Count == 0) Console.WriteLine("Stadium with this type not found.");
        else
        {
            var i = 0;
            Console.WriteLine("Stadiums: ");
            foreach (var stadium in found)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(stadium);
            }
        }
    }
}