using Practice_8.Database.Entities;

namespace Practice_8.Database.IndexSystem;

public class Indexing
{
    private readonly List<dynamic> _list = new();

    public void Append<T>(Index<T> index) where T : BaseEntity
    {
        _list.Add(index);
    }

    public bool HasDependencies(Type type)
    {
        return true;
    }
}