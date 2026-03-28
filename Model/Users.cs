public class User
{
    public int Id{get;set;}
    private string _name;
    private string _password;
    public string Name{get => _name;}
    public string Password{get => _password;}
    public User(string name, string password)
    {
        _name = name;
        _password = password;
    }
    public bool ChangeName(string name)
    {
        _name = name;
        return true;
    }
    public bool ChangePassword(string o_passw, string n_passw)
    {
        if(Password == o_passw)
        {
            _password = n_passw;
            return true;
        }
        return false;
    }
}

public class Admin : User
{
    public Admin(string name, string password) : base(name, password)
    {
    }
}