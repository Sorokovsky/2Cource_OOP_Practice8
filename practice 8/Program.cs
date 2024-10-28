using Practice_8.Commands;
using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Events;

namespace Practice_8;

public static class Program
{
    public static void Main()
    {
        UserEvents.NotLogined += SecurityCenter.UnAuthorized;
        CommandContext commandContext = new(new DBContext());
        commandContext.AddCommand(new ExitCommand());
        commandContext.Loop();
    }
}
