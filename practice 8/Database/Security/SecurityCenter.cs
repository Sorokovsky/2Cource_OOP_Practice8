using Practice_8.Database.Exceptions;

namespace Practice_8.Database.Security;

public class SecurityCenter
{
    private Repository<User> _users = new Repository<User>("users.dat");

    public User login(string login, string password)
    {
        var foundUsers = _users.List.Where(x => x.Login.Equals(login));
        if (!foundUsers.Any()) throw new UserNotFoundException();
        var withCorrectPasswords = foundUsers.Where(x => x.Password.Equals(password));
        if(!withCorrectPasswords.Any()) throw new InvalidPasswordException();
        return withCorrectPasswords.First();
    }

    public SecurityCenter()
    {
        _users.Load();
    }
}