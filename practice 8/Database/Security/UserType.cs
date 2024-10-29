namespace Practice_8.Database.Security;

[Serializable]
public class UserType
{
    public string Name { get; init; }
    
    public int Index { get; init; }

    public UserType(string name, int index)
    {
        Name = name;
        Index = index;
    }

    public static UserType Create(string name)
    {
        return SecurityCenter.Hierarchy.Find(name);
    }

    public static bool operator <(UserType first, UserType second)
    {
        return first.Index < second.Index;
    }
    
    public static bool operator >(UserType first, UserType second)
    {
        return first.Index > second.Index;
    }
    
    public static bool operator <=(UserType first, UserType second)
    {
        return first.Index <= second.Index;
    }
    
    public static bool operator >=(UserType first, UserType second)
    {
        return first.Index >= second.Index;
    }

    public override string ToString()
    {
        return $"{Name}\n";
    }
}