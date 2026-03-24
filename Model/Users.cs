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
    public string Password{get;set;}
    private readonly MyArray<Role> _roles = new();
    public MyArray<Role> Roles => _roles;

    public User(int id, string name, string password)
    {
        Id = id;
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be empty");
        if (string.IsNullOrEmpty(password))
            throw new ArgumentException("Name cannot be empty");
        Name = name;
        Password = password;
    }
    public void AddRole(Role role)
    {
        if(!_roles.Contains(role))
            _roles.Add(role);
    }
    public bool HasRole(Role role)
    {
        return _roles.Contains(role);
    }
}