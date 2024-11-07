namespace Practice_8.Database.IndexSystem;

public class Index
{
    public Type DependencyType { get; private set; }
    public Type DependsOnType { get; private set; }
    public string FieldNameInDependency { get; private set; }

    public Index(Type dependencyType, Type dependsOnType, string fieldNameInDependency)
    {
        DependencyType = dependencyType;
        DependsOnType = dependsOnType;
        FieldNameInDependency = fieldNameInDependency;
    }
}