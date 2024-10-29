using Practice_8.Database.Security;

namespace Practice_8.Database.Exceptions;

public class RoleNotExistsException : Exception
{
    public RoleNotExistsException(UserType role) : base(role.Name)
    {
        
    }
    
    public RoleNotExistsException(string role) : base($"Role: {role} not exists.")
    {
        
    }
}