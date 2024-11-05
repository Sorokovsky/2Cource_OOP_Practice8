using Practice_8.Commands;
using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace practice_8.Commands.StadiumTypeCommands;

public class UpdateStadiumTypeCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Update by name.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a name of stadium type: ");
        var name = Console.ReadLine() ?? string.Empty;
        var newType = StadiumTypeEntity.Enter();
        database.StadiumTypes.Update(x => x.Name.Equals(name), newType);
    }
}