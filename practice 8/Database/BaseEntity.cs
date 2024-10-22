namespace Practice_8.Database;

public class BaseEntity
{
    public int Id { get; } = _currentId++;

    private static int _currentId = 0;

    public override string ToString()
    {
        return $"Id: {Id}\n";
    }
}