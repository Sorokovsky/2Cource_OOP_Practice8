using Practice_8.Database.Entities;

namespace Practice_8.Events;

public static class EntityEvents
{
    public delegate void Operation(BaseEntity entity);

    public static event Operation? SuccessCreated;

    public static void OnSuccessCreated(BaseEntity entity)
    {
        SuccessCreated?.Invoke(entity);
    }
}