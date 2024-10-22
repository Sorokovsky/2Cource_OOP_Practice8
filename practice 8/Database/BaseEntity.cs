namespace Practice_8.Database;

public class BaseEntity
{
    public int Id { get; set; } = _currentId++;

    private static int _currentId = 0;
    
}