using Practice_8.Database.Exceptions;

namespace Practice_8.Database.Security;

public static class SecurityCenter
{
    private static Repository<User> _users = new Repository<User>("users.dat");
    
    public static User? CurrentUser { get; private set; }

    public static void Login(string login, string password)
    {
        var foundUsers = _users.List.Where(x => x.Login.Equals(login));
        if (!foundUsers.Any()) throw new UserNotFoundException();
        var withCorrectPasswords = foundUsers.Where(x => x.Password.Equals(password));
        if(!withCorrectPasswords.Any()) throw new InvalidPasswordException();
        CurrentUser = withCorrectPasswords.First();
    }

    public static void Logout()
    {
        CurrentUser = null;
    }

    public static void UnAuthorized()
    {
        Console.WriteLine("You are not authorized to log out. Please authorize.");
    }
}