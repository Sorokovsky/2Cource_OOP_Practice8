using Practice_8.Database.Entities;

namespace Practice_8.Models;

public class StadiumTypeModel
{
    public string Name { get; private set; }

    public StadiumTypeModel(StadiumTypeEntity entity)
    {
        Name = entity.Name;
    }

    public override string ToString()
    {
        return "Stadium type:\n" +
               $"Name: {Name}";
    }
}