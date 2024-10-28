namespace Practice_8.Events;

public static class UserEvents
{
    public delegate void Operation();

    public static event Operation NotLogined;

    public static void OnNotLogined()
    {
        NotLogined?.Invoke();
    }
}