using Practice_8.Database;
using Practice_8.Database.Exceptions;
using Practice_8.Database.Security;
using Practice_8.Events;

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
        var command = _commands.FirstOrDefault(x => x.Number == operation, null);
        if(command == null) throw new ArgumentException("Unknown operation. Try again.");
        if (SecurityCenter.Hierarchy.HasPermition(SecurityCenter.CurrentUser, command.NeedUserType))
        {
            command.Process(_database);
        }
        else
        {
            throw new InvalidRoleException(SecurityCenter.CurrentUser.Role, command.NeedUserType);
        }
    }

    public void Loop()
    {
        while (true)
        {
            try
            {
                Process(ChooseOperation());
            }
            catch (UserNotLoginnedException e)
            {
                UserEvents.OnNotLogined();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}