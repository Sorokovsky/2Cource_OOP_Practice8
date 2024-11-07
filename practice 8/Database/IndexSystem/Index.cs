namespace Practice_8.Database.IndexSystem;

public class Index
{
    public Type DependsOnType { get; private set; }
    
    public Type DependencyType { get; private set; }
    
    public string FieldName { get; private set; }

    public Index(Type dependsOnType, Type dependencyType, string fieldName)
    {
        DependsOnType = dependsOnType;
        DependencyType = dependencyType;
        FieldName = fieldName;
    }
}