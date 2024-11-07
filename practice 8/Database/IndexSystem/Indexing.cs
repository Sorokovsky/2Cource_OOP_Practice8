using System.Reflection;
using Practice_8.Database.Entities;

namespace Practice_8.Database.IndexSystem;

public class Indexing
{
    private static Indexing? _instance = null;
    
    private readonly List<Index> _list = new();
    private readonly DbContext _database;

    public Indexing(DbContext database)
    {
        _database = database;
        CollectIndexes();
    }

    public void Append(Index index)
    {
        _list.Add(index);
    }

    public bool HasDependencies(Type type)
    {
        return true;
    }

    private void CollectIndexes()
    {
        var repos = typeof(DbContext).GetProperties();
        foreach (var repo in repos)
        {
            var generic = repo.GetModifiedPropertyType().GenericTypeArguments.First();
            var entityType = Type.GetType(generic.FullName ?? string.Empty) ?? typeof(BaseEntity);
            var fields = entityType.GetProperties();
            foreach (var field in fields)
            {
                var name = field.Name;
                
            }
        }
    }
}