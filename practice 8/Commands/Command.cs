using Practice_8.Database;
using Practice_8.Database.Security;

namespace Practice_8.Commands;

public abstract class Command
{
    protected static int CurrentNumber;
    
    public abstract UserType NeedUserType { get; set; }

    public int Number { get; set; } = CurrentNumber++;

    public abstract string Title { get; set; }

    public abstract void Process(DbContext database, CommandContext currentContext);
}