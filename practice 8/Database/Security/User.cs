using Practice_8.Database.Entities;

namespace Practice_8.Database.Security;

[Serializable]
public class User : BaseEntity
{
    public string Login { get; set; }

    public string Password { get; set; }

    public User(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public override string ToString()
    {
        return $"{base.ToString()}" +
               $"Login: {Login}\n" +
               $"Password: {Password}\n";
    }
}