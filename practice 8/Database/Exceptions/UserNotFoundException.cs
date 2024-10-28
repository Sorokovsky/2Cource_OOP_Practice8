namespace Practice_8.Database.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("User not found")
    {
        
    }
}