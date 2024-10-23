namespace Practice_8.Database.Entities;

[Serializable]
public class PositionEntity : BaseEntity
{
    private int _salary;
    
    public string Name { get; set; } = string.Empty;

    public string Requirements { get; set; } = string.Empty;

    public int Salary
    {
        get => _salary;
        set => _salary = value < 0 ? 0 : value;
    }

    public PositionEntity(string name, string requirements, int salary)
    {
        Name = name;
        Requirements = requirements;
        Salary = salary;
    }

    public static PositionEntity Enter()
    {
        Console.Write("Enter a name of position: ");
        string name = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a requirements: ");
        string requirements = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a salary: ");
        int salary = Convert.ToInt32(Console.ReadLine());
        return new PositionEntity(name, requirements, salary);
    }

    public override string ToString()
    {
        return $"{base.ToString()}" +
               $"Name of position: {Name}\n" +
               $"Requirements: {Requirements}\n" +
               $"Salary: {Salary}uah";
    }
}