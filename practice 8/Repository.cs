using System.Collections;
using Practice_8.Utils;

namespace Practice_8;

public class Repository<T> : IEnumerable<T> where T : new() 
{
    private LinkedList<T> _list = new LinkedList<T>();
    private FileUtils<LinkedList<T>> _fileUtils;

    public int Count => _list.Count;

    public T this[int index]
    {
        get => _list.ElementAt(index);
    }
    
    public Repository(string filePath)
    {
        _fileUtils = new FileUtils<LinkedList<T>>(filePath);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _list.GetEnumerator();
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}