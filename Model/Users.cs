namespace Project_1.Model;
public enum Access
{
    Basic = 0,
    Admin = 1
}

public class User : IComparable<User>
{
    public int Id{get;private set;}
    // private string _name;
    // private string _password;
    public Access Role { get; set; }
    public string Name{get; private set;}
    public string Password{get; private set;}
    public User(int id, string name, string password)
    {
        Id = id;
        Name = name;
        Password = password;
        Role = Access.Basic;
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
    public void SetRole(Access newRole)
    {
        Role = newRole;
    }

    public int CompareTo(User? other)
    {
        if (other == null) return 1;
        return this.Id.CompareTo(other.Id);
    }

    public override string ToString()
    {
        return $"Id: {Id} | Name: {Name} | Role: {Role}";
    }
}
