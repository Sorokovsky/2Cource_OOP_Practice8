namespace Practice_8.Database.Entities;

[Serializable]
public class StadiumTypeEntity : BaseEntity
{
    public string Name { get; set; }
    
    public StadiumTypeEntity(string name)
    {
        Name = name;
    }

    public static StadiumTypeEntity Enter()
    {
        Console.Write("Enter a name of stadium type: ");
        return new StadiumTypeEntity(Console.ReadLine() ?? string.Empty);
    }
    
    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"Name: {Name}";
    }
}