namespace Practice_8.Events;

public static class UserEvents
{
    public delegate void Operation();

    public static event Operation NotLogined;

    public static event Operation InvalidPassword;

    public static event Operation InvalidLogin;

    public static event Operation SuccessLoginned;

    public static void OnNotLogined()
    {
        NotLogined?.Invoke();
    }

    public static void OnInvalidLogin()
    {
        InvalidLogin?.Invoke();
    }

    public static void OnInvalidPassword()
    {
        InvalidPassword?.Invoke();
    }

    public static void OnSuccessLoginned()
    {
        SuccessLoginned?.Invoke();
    }
}