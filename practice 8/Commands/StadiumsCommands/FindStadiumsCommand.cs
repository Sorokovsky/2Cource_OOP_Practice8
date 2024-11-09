using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.StadiumsCommands;

public class FindStadiumsCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Find stadium by code.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter code for finding stadium: ");
        var code = Convert.ToInt32(Console.ReadLine() ?? string.Empty);
        var found = database.Stadiums.List
            .Where(x => x.Code == code)
            .Join(
                database.StadiumTypes.List, 
                entity => entity.StadiumTypeId, 
                entity => entity.Id, 
                (entity, typeEntity) => new StadiumModel(entity, new StadiumTypeModel(typeEntity)))
            .ToList();
        if(found.Count == 0) Console.WriteLine("By this address do not found any stadium.");
        else
        {
            Console.WriteLine($"Stadiums by code = {code}");
            var i = 0;
            foreach (var stadium in found)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(stadium);
            }
        }
    }
}