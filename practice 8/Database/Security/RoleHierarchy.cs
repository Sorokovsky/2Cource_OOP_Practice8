using Practice_8.Database.Exceptions;

namespace Practice_8.Database.Security;

public class RoleHierarchy
{
    private List<UserType> _roles = new();

    public void Append(UserType role)
    {
        _roles.Add(role);
    }
    
    public bool HasPermition(User user, UserType needRole)
    {
        var isExistsUserRole = _roles.Any(x => FindRolePredicate(user.Role, needRole));
        var isExistsNeedRole = _roles.Any(x => FindRolePredicate(user.Role, needRole));
        if (isExistsUserRole == false) throw new RoleNotExistsException(user.Role);
        if (isExistsNeedRole == false) throw new RoleNotExistsException(needRole);
        return user.Role >= needRole;
    }

    public UserType Find(string name)
    {
        var candidate = _roles.FirstOrDefault(x => x.Name.Equals(name));
        if (candidate == null) throw new RoleNotExistsException(name);
        return candidate;
    }

    private bool FindRolePredicate(UserType current, UserType need)
    {
        return current.Index == need.Index && current.Name.Equals(need.Name);
    }
}