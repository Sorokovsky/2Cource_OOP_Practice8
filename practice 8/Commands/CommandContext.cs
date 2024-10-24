using Practice_8.Database;
using Practice_8.Database.Exceptions;
using Practice_8.Database.Security;

namespace Practice_8.Commands;

public class CommandContext
{
    private DBContext _database;

    public User? CurrentUser { get; set; } = null;
    
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
        if (CurrentUser == null) throw new UserNotLoginnedException();
        foreach (var command in _commands.Where(command => operation == command.Number && command.NeedUserType.Equals(CurrentUser.UserType)))
        {
            command.Process(_database);
            return;
        }

        throw new ArgumentException("Unknown operation. Try again.");
    }

    public void Loop()
    {
        while (true)
        {
            try
            {
                Process(ChooseOperation());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}