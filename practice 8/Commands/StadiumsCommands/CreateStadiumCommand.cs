using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;
using Practice_8.Events;

namespace Practice_8.Commands.StadiumsCommands;

public class CreateStadiumCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Create new stadium";
    
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var newStadium = StadiumEntity.Enter();
        Console.WriteLine("Choose a stadium type.");
        var i = 0;
        foreach (var type in database.StadiumTypes.List)
        {
            Console.WriteLine($"Index: {i++}");
            Console.WriteLine(type);
        }

        Console.Write("Enter a index: "); var index = Convert.ToInt32(Console.ReadLine());
        while (index < 0 || index >= database.StadiumTypes.Count)
        {
            Console.WriteLine("Invalid index. Try again: ");
            index = Convert.ToInt32(Console.ReadLine());
        }
        newStadium.StadiumTypeId = database.StadiumTypes[index].Id;
        database.Stadiums.Append(newStadium);
        EntityEvents.OnSuccessCreated(newStadium);
    }
}