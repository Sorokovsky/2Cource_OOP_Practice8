using Practice_8.Database.Exceptions;
using Practice_8.Events;

namespace Practice_8.Database.Security;

public static class SecurityCenter
{
    private static readonly Repository<User> Users = new Repository<User>("users.dat");

    public static readonly RoleHierarchy Hierarchy = new RoleHierarchy();
    
    public static User? CurrentUser { get; private set; }

    public static void Login(string login, string password)
    {
        try
        {
            var foundUsers = Users.List.Where(x => x.Login.Equals(login));
            var users = foundUsers as User[] ?? foundUsers.ToArray();
            if (users.Length == 0) throw new UserNotFoundException();
            var withCorrectPasswords = users.Where(x => x.Password.Equals(password));
            var correctPasswords = withCorrectPasswords as User[] ?? withCorrectPasswords.ToArray();
            if (correctPasswords.Length == 0) throw new InvalidPasswordException();
            CurrentUser = correctPasswords.First();
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
        CurrentUser = new User("Quest", "", UserType.Create(Roles.Quest));
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
        Logout();
    }
}