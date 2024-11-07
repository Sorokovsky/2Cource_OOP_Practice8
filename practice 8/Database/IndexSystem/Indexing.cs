using System.Reflection;
using Practice_8.Database.Entities;

namespace Practice_8.Database.IndexSystem;

public class Indexing
{
    private readonly List<Index> _list = new();
    private readonly DbContext _database;

    public Indexing(DbContext database)
    {
        _database = database;
        CollectIndexes();
    }

    private void Append(Index index)
    {
        _list.Add(index);
    }

    public List<BaseEntity> GetDependencies(BaseEntity entity)
    {
        List<BaseEntity> dependencies = new();
        Type type = entity.GetType();
        var found = _list.Where(x => x.DependsOnType.Name.Equals(type.Name)).ToList();
        foreach (var index in found)
        {
            var dependency = index.DependencyType;
            var field = typeof(DbContext).GetProperties()
                .First(x => x.Name.Equals(dependency.Name.Replace("Entity", "s")));
            var idField = dependency.GetProperties()
                .First(x => x.Name.Equals(index.FieldName));
            dynamic result = field.GetValue(_database)!;
            foreach (var item in result.List)
            {
                if (idField.GetValue(item) == entity.Id)
                {
                    dependencies.Add(item);
                }
            }
        }
        return dependencies;
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
                ProcessField(field, entityType);
            }
        }
    }

    private void ProcessField(PropertyInfo field, Type currentEntityType)
    {
        var types = GetEntityTypes();
        foreach (var type in types.Where(type => IsKey(field, type)))
        {
            var index = new Index(type, currentEntityType, field.Name);
            Append(index);
        }
    }

    private List<Type> GetEntityTypes()
    {
        var list = new List<Type>();
        var repos = typeof(DbContext).GetProperties();
        foreach (var repo in repos)
        {
            var generic = repo.GetModifiedPropertyType().GenericTypeArguments.First();
            var entityType = Type.GetType(generic.FullName ?? string.Empty) ?? typeof(BaseEntity);
            list.Add(entityType);
        }

        return list;
    }

    private bool IsKey(PropertyInfo field, Type type)
    {
        return field.Name.EndsWith($"{type.Name.Replace("Entity", "")}Id");
    }
}