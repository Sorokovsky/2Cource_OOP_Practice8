namespace Practice_8.Database.Entities;

[Serializable]
public class TeamEntity : BaseEntity
{
    public string Name { get; set; }
    
    public int CoachId { get; set; }

    public TeamEntity(string name)
    {
        Name = name;
    }

    public static TeamEntity Enter()
    {
        Console.Write("Enter a name: ");
        var name = Console.ReadLine() ?? string.Empty;
        return new TeamEntity(name);
    }

    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"Name: {Name}\n" +
               $"Coach id: {CoachId}\n";
    }
}