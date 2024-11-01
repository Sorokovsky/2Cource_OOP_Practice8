using Practice_8.Database.Exceptions;

namespace Practice_8.Database.Security;

public class RoleHierarchy
{
    private List<UserType> _roles = new();
    
    public IReadOnlyList<UserType> Roles => _roles;

    public void Append(UserType role)
    {
        _roles.Add(role);
    }
    
    public bool HasPermission(User user, UserType needRole)
    {
        ValidateRole(user.Role);
        ValidateRole(needRole);
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

    private void ValidateRole(UserType role)
    {
        var isExistsRole = _roles.Any(x => FindRolePredicate(x, role));
        if (isExistsRole == false) throw new RoleNotExistsException(role);
    }
}