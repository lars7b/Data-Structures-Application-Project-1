using Project_1.Repository;

public interface ILoginService
{
    User Login(string name, string password);
}

public class LoginService : ILoginService
{
    UserRepository _userRepository;
    public LoginService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User Login(string name, string password)
    {
        var user = _userRepository.GetByName(name);
        if(user == null) return null;
        if(user.Password != password) return null;
        else
        {
            return user;
        }
    }
}