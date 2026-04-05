namespace Project_1.Model;
public enum Access
{
    Basic = 0,
    Admin = 1
}

public class User
{
    public int Id{get;private set;}
    // private string _name;
    // private string _password;
    private Access _role;
    public Access Role{get => _role;}
    public string Name{get; private set;}
    public string Password{get; private set;}
    public User(int id, string name, string password)
    {
        Id = id;
        Name = name;
        Password = password;
        _role = Access.Basic;
    }
    public bool ChangeName(string name)
    {
        if(string.IsNullOrWhiteSpace(name)) return false;
        Name = name;
        return true;
    }
    public bool ChangePassword(string o_passw, string n_passw)
    {
        if(string.IsNullOrWhiteSpace(o_passw)) return false;
        if(Password == o_passw)
        {
            if(string.IsNullOrWhiteSpace(n_passw)) return false;
            Password = n_passw;
            return true;
        }
        return false;
    }
    internal void SetRole(Access newRole)
    {
        _role = newRole;
    }
}