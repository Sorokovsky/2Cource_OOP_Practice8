using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Commands;

public class CreateStadiumTypeCommand : Command
{
    public override UserType NeedUserType { get; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Create a stadium type.";
    public override void Process(DbContext database)
    {
        var stadiumType = StadiumTypeEntity.Enter();
        database.StadiumTypes.Append(stadiumType);
    }
}