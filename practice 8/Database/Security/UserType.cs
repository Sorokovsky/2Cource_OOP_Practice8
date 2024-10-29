namespace Practice_8.Database.Security;

public class UserType
{
    public string Name { get; set; }
    
    public int Index { get; set; }

    public UserType(string name, int index)
    {
        Name = name;
        Index = index;
    }

    public override string ToString()
    {
        return $"Role: {Name}\n";
    }
}