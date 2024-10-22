using Practice_8.Database;

namespace Practice_8.Commands;

public class CommandContext
{
    private DBContext _database;
    
    private readonly List<Command> _commands = new();

    public CommandContext(DBContext database)
    {
        _database = database;
    }

    public void AddCommand(Command command)
    {
        _commands.Add(command);
    }

    private int ChooseOperation()
    {
        Console.WriteLine("Choose operation.");
        foreach (var command in _commands)
        {
            Console.WriteLine($"{command.Number}-{command.Title}");
        }

        Console.Write(">> ");
        return Convert.ToInt32(Console.ReadLine());
    }

    private void Process(int operation)
    {
        foreach (var command in _commands)
        {
            if (operation == command.Number)
            {
                command.Process(_database);
                return;
            }
        }
        throw new ArgumentException("Unknown operation. Try again.");
    }

    public void Loop()
    {
        while (true)
        {
            try
            {
                int operation = ChooseOperation();
                Process(operation);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}