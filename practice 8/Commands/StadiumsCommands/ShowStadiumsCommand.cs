using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Models;

namespace Practice_8.Commands.StadiumsCommands;

public class ShowStadiumsCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show stadiums.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        if(database.Stadiums.List.Count == 0) Console.WriteLine("No stadiums found.");
        else
        {
            var stadiums = database.Stadiums.List
                .Join(
                    database.StadiumTypes.List,
                    stadium => stadium.StadiumTypeId,
                    type => type.Id,
                    (entity, typeEntity) => new StadiumModel(entity, new StadiumTypeModel(typeEntity))
                );
            int i = 0;
            foreach (var stadium in stadiums)
            {
                Console.WriteLine($"#{++i}");
                Console.WriteLine(stadium);
            }
        }
    }
}