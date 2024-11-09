using Practice_8.Database;
using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Commands;

public abstract class Command
{
    protected static int CurrentNumber;
    
    public abstract UserType NeedUserType { get; set; }

    public int Number { get; set; } = CurrentNumber++;

    public abstract string Title { get; set; }

    public abstract void Process(DbContext database, CommandContext currentContext);

    protected virtual T ChooseDependsOn<T>(string title, Repository<T> repository) where T : BaseEntity
    {
        Console.WriteLine($"Choose a {title} type.");
        var i = 0;
        foreach (var type in repository.List)
        {
            Console.WriteLine($"Index: {i++}");
            Console.WriteLine(type);
        }
        Console.Write("Enter a index or -1 to create new: "); var index = Convert.ToInt32(Console.ReadLine());
        while (index < -1 || index >= repository.Count)
        {
            Console.WriteLine("Invalid index. Try again: ");
            index = Convert.ToInt32(Console.ReadLine());
        }

        if (index == -1)
        {
            var type = Type.GetType(repository.GetType().GenericTypeArguments.First().FullName ?? typeof(BaseEntity).FullName) ?? typeof(BaseEntity);
            var instance = Activator.CreateInstance(type);
            if (instance != null)
            {
                dynamic newItem = instance.GetType().GetMethod("Enter").Invoke(instance, null);
                return repository.Append(newItem);
            }
        }
        return repository[index];
    }
}