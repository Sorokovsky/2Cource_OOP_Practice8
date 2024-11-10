using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.StadiumsCommands;

public class ChangeTypeCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Change a stadium type.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a code of stadium to find: ");
        var code = Convert.ToInt32(Console.ReadLine());
        var found = database.Stadiums.List.Where(x => code == x.Code).ToList();
        if(found.Count == 0) Console.WriteLine($"Stadium by code == {code} not found.");
        else
        {
            var type = ChooseDependsOn("stadium type", database.StadiumTypes);
            foreach (var stadium in found)
            {
                stadium.StadiumTypeId = type.Id;
                database.Stadiums.Update(x => x.Id == stadium.Id, stadium);   
            }
        }
    }
}