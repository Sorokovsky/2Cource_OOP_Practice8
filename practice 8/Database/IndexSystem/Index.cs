using Practice_8.Database.Entities;

namespace Practice_8.Database.IndexSystem;

public class Index<T> where T : BaseEntity
{
    public delegate int GetIndex(T dependency);
    
    public Type DependOnType { get; set; }

    public GetIndex GetFromDependency { get; set; }

    public Index(Type dependOnType, GetIndex getFromDependency)
    {
        DependOnType = dependOnType;
        GetFromDependency = getFromDependency;
    }
}