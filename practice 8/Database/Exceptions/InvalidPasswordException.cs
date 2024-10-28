namespace Practice_8.Database.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() : base("Invalid password.")
    {
        
    }
}