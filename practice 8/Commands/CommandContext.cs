using Practice_8.Database;
using Practice_8.Database.Exceptions;
using Practice_8.Database.Security;
using Practice_8.Events;

namespace Practice_8.Commands;

public class CommandContext
{
    private DbContext _database;
    
    private readonly List<Command> _commands = new();

    public CommandContext(DbContext database)
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
        var defaultUser = new User("Quest", "quest", UserType.Create(Roles.Quest));
        var user = SecurityCenter.CurrentUser ?? defaultUser;
        var command = _commands.FirstOrDefault(x => x?.Number == operation, null);
        if(command == null) throw new ArgumentException("Unknown operation. Try again.");
        if (SecurityCenter.Hierarchy.HasPermission(user, command.NeedUserType))
        {
            command.Process(_database);
        }
        else
        {
            throw new InvalidRoleException(user.Role, command.NeedUserType);
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
            catch (UserNotLoginnedException)
            {
                UserEvents.OnNotLoginned();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }
}