using System.Collections.ObjectModel;
using Practice_8.Database.Utils;
using Practice_8.Database.Entities;
using Practice_8.Events;

namespace Practice_8.Database;

public class Repository<T> where T : BaseEntity
{
    public delegate bool IsNeed(T item);
    
    private LinkedList<T> _list;
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
        _list = new LinkedList<T>();
        _primaryKey = new PrimaryKey(0);
        Load();
    }

    public void Append(T newItem)
    {
        newItem.Id = _primaryKey.NewId;
        _list.AddLast(newItem);
        Save();
        EntitySuccessEvents.OnCreated(newItem);
    }

    public void Update(IsNeed isNeed, T updatedItem)
    {
        var found = _list.Where(isNeed.Invoke).ToList();
        foreach (var entity in found)
        {
            updatedItem.Id = entity.Id;
            _list.Remove(entity);
            _list.AddLast(updatedItem);
            Sort();
            Save();
            EntitySuccessEvents.OnUpdated(updatedItem);
        }
    }

    private void Sort()
    {
        var temp = _list.ToList();
        temp.Sort((first, second) => first.Id.CompareTo(second.Id));
        _list = new LinkedList<T>(temp);
    }
    
    private void Save()
    {
        _fileUtils.WriteToFile(_list);
    }

    private void Load()
    {
        _list = _fileUtils.ReadFromFile();
        Sort();
        _primaryKey = new PrimaryKey(_list.Count == 0 ? 0 : _list.Max(x => x.Id)); 
    }
}