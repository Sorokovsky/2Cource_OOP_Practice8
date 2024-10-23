namespace Practice_8.Database.Security;

public static class SecurityCenter
{
    private static Repository<User> _users = new Repository<User>("users.dat");

    public static void Verify(User user)
    {
        
    }
}