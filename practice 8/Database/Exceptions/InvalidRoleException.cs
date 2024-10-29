using Practice_8.Database.Security;

namespace Practice_8.Database.Exceptions;

public class InvalidRoleException : Exception
{
    public InvalidRoleException(UserType current, UserType need) : base($"Current role: {current}, Need role: {need}")
    {
        
    }
}