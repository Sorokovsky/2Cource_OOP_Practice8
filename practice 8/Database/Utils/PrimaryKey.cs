namespace Practice_8.Database.Utils;

public class PrimaryKey
{
    private int _currentId = 0;

    public int NewId => ++_currentId;

    public PrimaryKey(int lastId)
    {
        _currentId = lastId;
    }

    public PrimaryKey() : this(0)
    {
        
    }
}