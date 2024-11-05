using Practice_8.Database.Entities;

namespace Practice_8.Events;

public static class EntitySuccessEvents
{
    public delegate void Operation(BaseEntity entity);

    public static event Operation? Created;

    public static void OnCreated(BaseEntity entity)
    {
        Created?.Invoke(entity);
    }
}