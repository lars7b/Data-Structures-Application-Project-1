using Project_1.Repository;

public interface ILoginService
{
    bool IsAdmin { get; set; }
    bool LoggedIn { get; set; }
    User? Luser{get;set;}
    User? Login(string name, string password);
    void Logout();
}

public class LoginService : ILoginService
{
    public bool LoggedIn { get; set; }
    public bool IsAdmin { get; set; }
    private User? _loggedinUser;
    private User _admin;
    public User? Luser
    {
        get
        {
            return _loggedinUser;
        }
        set
        {
            _loggedinUser = value;
        }
    }

    public User? loggedIn { get; set; }

    UserRepository _userRepository;
    public LoginService(UserRepository userRepository)
    {
        _userRepository = userRepository;
        _admin = new User(0, "Admin", "Secret");
    }

    public User? Login(string name, string password)
    {
        if(_admin.Name == name && _admin.Password == password)
        {
            IsAdmin = true;
            LoggedIn = true;
            Luser = _admin;
        }
        var user = _userRepository.GetByName(name);
        if(user == null) return null;
        if(user.Password != password) return null;
        Luser = user;
        IsAdmin = false;
        LoggedIn = true;
        return Luser;
    }
    public void Logout()
    {
        Luser = null;
        LoggedIn = false;
        IsAdmin = false;
    }
}