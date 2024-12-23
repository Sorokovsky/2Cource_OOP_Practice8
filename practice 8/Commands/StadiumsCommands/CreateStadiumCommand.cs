using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Commands.StadiumsCommands;

public class CreateStadiumCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Create new stadium.";
    
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var newStadium = StadiumEntity.Enter();
        newStadium.StadiumTypeId = ChooseDependsOn("stadium type", database.StadiumTypes).Id;
        database.Stadiums.Append(newStadium);
    }
}