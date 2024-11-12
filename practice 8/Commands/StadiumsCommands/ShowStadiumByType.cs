using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.StadiumsCommands;

public class ShowStadiumByType : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show by type.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name of type: ");
        var name = Console.ReadLine() ?? string.Empty;
        var types = database.StadiumTypes.List
            .Where(x => x.Name.Equals(name))
            .ToList();
        var found = new List<StadiumModel>();
        types.ForEach(type =>
        {
            var stadiums = database.Stadiums.List
                .Where(stadium => stadium.StadiumTypeId == type.Id)
                .ToList();
            stadiums.ForEach(x => found.Add(new StadiumModel(x, new StadiumTypeModel(type))));
        });
        if(found.Count == 0) Console.WriteLine("No stadiums.");
        else
        {
            Console.WriteLine("Stadiums");
            var i = 0;
            foreach (var stadium in found)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(stadium);
            }
        }
    }
}