using Practice_8.Database;

namespace Practice_8.Commands;

public class ContextConfigure
{
    private static int _currentNumber = 0;
    private static int NewNumber => _currentNumber++;
    private CommandContext _instance;

    public ContextConfigure(DbContext database)
    {
        _instance = new CommandContext();
    }
}