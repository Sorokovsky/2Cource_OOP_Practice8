namespace Practice_8.Database.Exceptions;

public class UserNotLoginnedException : Exception
{
    public UserNotLoginnedException() : base("Unknown user. please login.")
    {
        
    }
}