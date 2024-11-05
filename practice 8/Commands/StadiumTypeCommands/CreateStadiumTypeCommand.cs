using Practice_8.Commands;
using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace practice_8.Commands.StadiumTypeCommands;

public class CreateStadiumTypeCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Create a stadium type.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var stadiumType = StadiumTypeEntity.Enter();
        database.StadiumTypes.Append(stadiumType);
    }
}