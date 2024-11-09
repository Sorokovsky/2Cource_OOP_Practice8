using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Commands.StadiumsCommands;

public class UpdateStadiumCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Update stadium.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a code of stadium for found: ");
        var code = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter a new data for stadium.");
        var newStadium = StadiumEntity.Enter();
        newStadium.StadiumTypeId = ChooseDependsOn("stadium type", database.StadiumTypes).Id;
        database.Stadiums.Update(x => x.Code == code, newStadium);
    }
}