using Practice_8.Commands;
using Practice_8.Database;

namespace Practice_8;

public static class Program
{
    public static void Main()
    {
        CommandContext commandContext = new(new DBContext());
        commandContext.AddCommand(new ExitCommand());
        commandContext.Loop();
    }
}
