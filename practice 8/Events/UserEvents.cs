namespace Practice_8.Events;

public static class UserEvents
{
    public delegate void Operation();

    public static event Operation? NotLoginned;

    public static event Operation? InvalidPassword;

    public static event Operation? InvalidLogin;

    public static event Operation? SuccessLoginned;

    public static event Operation? SuccessLogout;

    public static void OnNotLoginned()
    {
        NotLoginned?.Invoke();
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

    public static void OnSuccessLogout()
    {
        SuccessLogout?.Invoke();
    }
}