using Practice_8.Commands;
using Practice_8.Database;
using Practice_8.Database.Security;

namespace practice_8.Commands.StadiumTypeCommands;

public class ShowStadiumTypesCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.User);
    public override string Title { get; set; } = "Show stadium types.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        var types = database.StadiumTypes.List;
        if(types.Count == 0) Console.WriteLine("No stadium types found.");
        else Console.WriteLine("Stadium types:");
        var i = 0;
        foreach (var type in types)
        {
            Console.WriteLine($"#{++i}");
            Console.WriteLine(type);
        }
    }
}