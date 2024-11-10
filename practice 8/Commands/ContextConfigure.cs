using Practice_8.Database;
using Practice_8.Database.Security;
using Practice_8.Database.Utils;

namespace Practice_8.Commands;

public class ContextConfigure
{
    private CommandContext? _instance;
    private readonly DbContext _database;
    private readonly PrimaryKey _primaryKey = new PrimaryKey(-1);

    public ContextConfigure(DbContext database)
    {
        _database = database;
    }

    public void Create()
    {
        _instance = new CommandContext(_database, string.Empty);
    }

    public void WithTitle(string title)
    {
        ValidateInstance();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        _instance.Title = title;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private void ValidateInstance()
    {
        if (_instance == null)
        {
            throw new NullReferenceException("Instance is null");
        }   
    }

    public void WithCommands(params Command[] commands)
    {
        ValidateInstance();
        foreach (var command in commands)
        {
            command.Number = _primaryKey.NewId;
            _instance?.AddCommand(command);
        }
    }

    public CommandContext Build()
    {
        ValidateInstance();
        var result = _instance;
        _instance = null;
#pragma warning disable CS8603 // Possible null reference return.
        return result;
    }
}