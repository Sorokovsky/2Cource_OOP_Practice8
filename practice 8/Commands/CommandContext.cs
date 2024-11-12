using Practice_8.Database;
using Practice_8.Database.Exceptions;
using Practice_8.Database.Security;
using Practice_8.Events;

namespace Practice_8.Commands;

public class CommandContext : Command
{
    private DbContext _database;
    private bool _isActive;
    
    private readonly List<Command> _commands = new();

    public CommandContext(DbContext database, string title)
    {
        _database = database;
        // ReSharper disable once VirtualMemberCallInConstructor
        Title = title;
        CurrentNumber = 0;
        // ReSharper disable once VirtualMemberCallInConstructor
        SetMinimalRole();
    }

    private void SetMinimalRole()
    {
        NeedUserType = _commands.Where(x => x is ExitCommand == false)
            .Min(x => x.NeedUserType) 
                       ?? 
                       (SecurityCenter.Hierarchy.Roles.Min() ?? UserType.Create(Roles.Quest));
    }

    public void AddCommand(Command command)
    {
        _commands.Add(command);
        SetMinimalRole();
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
        var defaultUser = new User(Roles.Quest, Roles.Quest.ToLower(), UserType.Create(Roles.Quest));
        var user = SecurityCenter.CurrentUser ?? defaultUser;
        var command = _commands.FirstOrDefault(x => x?.Number == operation, null);
        if(command == null) throw new ArgumentException("Unknown operation. Try again.");
        if (SecurityCenter.Hierarchy.HasPermission(user, command.NeedUserType))
        {
            command.Process(_database, this);
        }
        else
        {
            throw new InvalidRoleException(user.Role, command.NeedUserType);
        }
    }

    public void Loop()
    {
        _isActive = true;
        Console.WriteLine(Title);
        while (_isActive)
        {
            try
            {
                Process(ChooseOperation());
                if (_isActive == false) continue;
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
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
    }

    public void Exit()
    {
        _isActive = false;
    }

    public override UserType NeedUserType { get; set; }
    public override string Title { get; set; }
    public override void Process(DbContext database, CommandContext currentContext)
    {
        Loop();
    }
}