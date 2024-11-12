using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.StadiumsCommands;

public class RemoveStadiumCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Remove by code.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a code of stadium for removing: ");
        var code = Convert.ToInt32(Console.ReadLine());
        database.Stadiums.Remove(x => x.Code == code);
    }
}