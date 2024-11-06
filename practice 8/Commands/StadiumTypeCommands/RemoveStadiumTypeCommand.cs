using Practice_8.Commands;
using Practice_8.Database;
using Practice_8.Database.Security;

namespace practice_8.Commands.StadiumTypeCommands;

public class RemoveStadiumTypeCommand : Command 
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Remove by name.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name of type for deleting: ");
        var name = Console.ReadLine() ?? string.Empty;
        database.StadiumTypes.Remove(x => x.Name.Equals(name));
    }
}