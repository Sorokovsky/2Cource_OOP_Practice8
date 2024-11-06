using Practice_8.Database.Entities;

namespace Practice_8.Events;

public static class EntitySuccessEvents
{
    public delegate void Operation(BaseEntity entity);

    public static event Operation? Created;

    public static event Operation? Updated;

    public static event Operation? Removed;

    public static void OnCreated(BaseEntity entity)
    {
        Created?.Invoke(entity);
    }

    public static void OnUpdated(BaseEntity entity)
    {
        Updated?.Invoke(entity);
    }

    public static void OnRemoved(BaseEntity entity)
    {
        Removed?.Invoke(entity);
    }
}