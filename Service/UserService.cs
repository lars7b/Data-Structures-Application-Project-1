using Project_1.Collections;
using Project_1.Repository;

namespace Project_1.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly MyArray<User> _users;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
        _users = _repository.LoadAllUsers();
    }

    public void AddUser(string name, string password)
    {
        var newId = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
        var newUser = new User(newId, name, password);
        _users.Add(newUser);
        _repository.SaveUsers(_users);
    }

    public int Count()
    {
        return _users.Count;
    }

    public IMyCollection<User> GetAllUsers()
    {
        return _users;
    }

    public Result<User> FindUser(string name)
    {
        var user = _users.FindBy(name, (user, key) => user.Name == key)?.Value;
        if (user != null) return new Result<User>(true, user, "");
        return new Result<User>(false, null, "User does not exists or cannot be found");
    }
    private User? FindByName(string name)
    {
        return _users.FindBy(name, (user, key) => user.Name == key)?.Value;
    }
    public bool RemoveUser(string name)
    {
        var user = FindByName(name);
        if(user == null) return false;
        _users.Remove(user);
        _repository.SaveUsers(_users);
        return true;
    }   
}
public record Result<T>(bool isSucces, T? Value, string? Error);