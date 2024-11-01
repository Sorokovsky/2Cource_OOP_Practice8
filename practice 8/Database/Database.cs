using Practice_8.Database.Security;

namespace Practice_8.Database;

public class DBContext
{
    public Repository<User> Users { get; } = new Repository<User>("users.dat");
}