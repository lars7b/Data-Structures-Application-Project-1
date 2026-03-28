public enum Access
{
    Basic = 0,
    Admin = 1
}

public class User
{
    public int Id{get;private set;}
    private string _name;
    private string _password;
    private Access _role;
    public Access Role{get => _role;}
    public string Name{get => _name;}
    public User(int id, string name, string password)
    {
        Id = id;
        _name = name;
        _password = password;
        _role = Access.Basic;
    }
    public bool ChangeName(string name)
    {
        if(string.IsNullOrWhiteSpace(name)) return false;
        _name = name;
        return true;
    }
    public bool ChangePassword(string o_passw, string n_passw)
    {
        if(string.IsNullOrWhiteSpace(o_passw)) return false;
        if(_password == o_passw)
        {
            if(string.IsNullOrWhiteSpace(n_passw)) return false;
            _password = n_passw;
            return true;
        }
        return false;
    }
}