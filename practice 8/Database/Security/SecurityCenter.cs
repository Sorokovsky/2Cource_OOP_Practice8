using Practice_8.Database.Exceptions;
using Practice_8.Events;

namespace Practice_8.Database.Security;

public static class SecurityCenter
{
    private static Repository<User> _users = new Repository<User>("users.dat");

    public static RoleHierarchy Hierarchy = new RoleHierarchy();

    public static User CurrentUser { get; private set; }

    public static void Login(string login, string password)
    {
        try
        {
            var foundUsers = _users.List.Where(x => x.Login.Equals(login));
            if (!foundUsers.Any()) throw new UserNotFoundException();
            var withCorrectPasswords = foundUsers.Where(x => x.Password.Equals(password));
            if (!withCorrectPasswords.Any()) throw new InvalidPasswordException();
            CurrentUser = withCorrectPasswords.First();
            UserEvents.OnSuccessLoginned();
        }
        catch (UserNotFoundException)
        {
            UserEvents.OnInvalidLogin();
        }
        catch (InvalidPasswordException)
        {
            UserEvents.OnInvalidPassword();
        }
    }

    public static void Logout()
    {
        CurrentUser = null;
    }

    public static void UnAuthorized()
    {
        Console.WriteLine("You are not authorized to log out. Please authorize.");
    }
    
    public static void PrepareRoles()
    {
        Hierarchy.Append(new UserType(Roles.Quest, 0));
        Hierarchy.Append(new UserType(Roles.User, 1));
        Hierarchy.Append(new UserType(Roles.Admin, 2));
        CurrentUser = new User("Quest", "", UserType.Create(Roles.Quest));
    }
}