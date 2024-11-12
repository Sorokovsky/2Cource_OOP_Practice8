using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands.StadiumsCommands;

public class ChangeStadiumPhoneNumberCommand : Command
{
    public override UserType NeedUserType { get; set; } = UserType.Create(Roles.Admin);
    public override string Title { get; set; } = "Change phone number.";
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Console.Write("Enter a code of stadium: ");
        var code = Convert.ToInt32(Console.ReadLine());
        var found = database.Stadiums.List.Where(x => x.Code == code).ToList();
        if(found.Count == 0) Console.WriteLine("No one stadium.");
        else
        {
            Console.Write("Enter a new phone number: ");
            var phone = Console.ReadLine() ?? string.Empty;
            foreach (var stadium in found)
            {
                stadium.PhoneNumber = phone;
                database.Stadiums.Update(x => x.Id == stadium.Id, stadium);
            }
        }
    }
}