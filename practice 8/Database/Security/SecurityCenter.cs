namespace Practice_8.Database.Security;

public class SecurityCenter
{
    private Repository<User> _users = new Repository<User>("users.dat");

    public void Verify(User user)
    {
        
    }

    public SecurityCenter()
    {
        _users.Load();
    }
}