using Project_1.Repository;

public class LoginService
{
    private readonly UserRepository _userRepository;
    public User? CurrentUser{get;set;}
    public LoginService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public bool Login(string name)
    {
        var result = _userRepository.GetUserByUsername(name);
        if (result.Succes)
        {
            CurrentUser = result.Value;
            return true;
        }
        return false;
    }
    public bool Logout()
    {
        CurrentUser = null;
        return true;
    }
    public Access CheckRoles()
    {
        return CurrentUser.Role;
    }
    public bool CompareNames(string name)
    {
        var result = _userRepository.GetUserByUsername(name);
        if(result.Succes && result.Value.Name == CurrentUser.Name)
        {
            return true;
        }
        return false;
    }
}