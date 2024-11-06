using Practice_8.Database.Entities;

namespace Practice_8.Database.Utils;

public class Indexing
{
    public delegate int GetForeginId<T>(T entity) where T : BaseEntity;
    
    private Dictionary<BaseEntity, object> _indexes = new();

    public void Append<T, V>(T dependenceOn, GetForeginId<V> getForeginId) where T : BaseEntity where V : BaseEntity
    {
        _indexes.Add(dependenceOn, getForeginId);
    }
}