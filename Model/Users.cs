using System.Security.Cryptography.X509Certificates;
using Project_1.Collections;

public enum Role
{
    Admin, //0
    Dev //1
}

public class User
{
    public int Id{get;private set;}
    public string Name{get;private set;}
    private readonly MyArray<Role> _roles = new();
    public MyArray<Role> Roles => _roles;

    public User(string name)
    {
        Name = name;
    }
    public void AddRole(Role role)
    {
        if(_roles.Contains(role))
            _roles.Add(role);
    }
    public bool HasRole(Role role)
    {
        return _roles.Contains(role);
    }
}