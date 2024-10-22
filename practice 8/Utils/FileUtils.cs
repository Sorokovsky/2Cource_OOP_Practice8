#pragma warning disable SYSLIB0011
using System.Runtime.Serialization.Formatters.Binary;
namespace Practice_8.Utils;

public class FileUtils<T> where T : new()
{
    private static string _filePath = String.Empty;

    public FileUtils(string filePath)
    {
        _filePath = filePath;
    }

    public void WriteToFile(T data)
    {

        var formatter = new BinaryFormatter();
        var mode = FileMode.Truncate;
        if (File.Exists(_filePath) == false) mode = FileMode.Create;
        using var stream = new FileStream(_filePath, mode);
        formatter.Serialize(stream, data);
    }

    public T ReadFromFile()
    {
        var formatter = new BinaryFormatter();
        using var stream = new FileStream(_filePath, FileMode.Open);
        T data = default(T);
        if (File.Exists(_filePath))
        {
            data = (T)formatter.Deserialize(stream);
        }

        return data;
    }
}