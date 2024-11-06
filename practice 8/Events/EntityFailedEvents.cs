using Practice_8.Database.Entities;

namespace Practice_8.Events;

public static class EntityFailedEvents
{
    public delegate void Operation(BaseEntity entity, string reason);

    public static event Operation NotDeleted;

    public static void OnNotDeleted(BaseEntity entity, string reason)
    {
        NotDeleted?.Invoke(entity, reason);
    }
}