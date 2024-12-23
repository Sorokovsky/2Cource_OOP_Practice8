using Practice_8.Database.Entities;

namespace Practice_8.Database.Security;

[Serializable]
public class User : BaseEntity
{
    public string Login { get; set; }

    public string Password { get; set; }

    public UserType Role { get; set; }

    public User(string login, string password, UserType role)
    {
        Login = login;
        Password = password;
        Role = role;
    }

    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"Login: {Login}\n" +
               $"Role: {Role}";
    }
}