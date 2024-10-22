using System.Collections.ObjectModel;
using Practice_8.Database.Utils;
using Practice_8.Database.Entities;

namespace Practice_8.Database;

public class Repository<T> where T : BaseEntity
{
    private readonly LinkedList<T> _list;
    private readonly FileUtils<LinkedList<T>> _fileUtils;
    private PrimaryKey _primaryKey;

    public int Count => _list.Count;

    public ReadOnlyCollection<T> List => _list.ToList().AsReadOnly();

    public T this[int index]
    {
        get => _list.ElementAt(index);
    }
    
    public Repository(string filePath)
    {
        _fileUtils = new FileUtils<LinkedList<T>>(filePath);
        _list = _fileUtils.ReadFromFile() ?? new LinkedList<T>();
        _primaryKey = new PrimaryKey(_list.Count == 0 ? 0 : _list.Max(x => x.Id));
    }

    public void Append(T item)
    {
        item.Id = _primaryKey.NewId;
        _list.AddLast(item);
    }
}