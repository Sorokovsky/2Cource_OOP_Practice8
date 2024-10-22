using Practice_8.Database;

namespace Practice_8.Commands;

public abstract class Command
{
    private static int _currentNumber = 0;

    public int Number { get; set; } = _currentNumber++;

    public abstract string Title { get; set; }

    public abstract void Process(DBContext database);
}