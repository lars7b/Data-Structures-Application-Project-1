using Project_1.Repository;
using Project_1.Model;

namespace Project_1.Service;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    public User? CurrentUser{get;set;}
    public LoginService(IUserRepository userRepository)
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
    public bool ChangeUserRole(User targetUser, Access newRole)
    {
        if(CheckRoles() != Access.Admin)
        {
            return false;
        }
        targetUser.SetRole(newRole);
        return true;
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