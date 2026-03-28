using Project_1.Collections;
using Project_1.Repository;

namespace Project_1.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMyCollection<User> _users;
    private int _nextId;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
        _users = _repository.LoadAllUsers();
        _nextId  = _users.Count == 0 ? 1 : _users.Max(_=>_.Id) +1;
    }

    public bool AddUser(string name, string password)
    {
        if(string.IsNullOrWhiteSpace(name)||string.IsNullOrWhiteSpace(password)) return false;
        _users.Add(new User(_nextId,name, password));
        _nextId++;
        _repository.SaveUsers(_users);
        return true;
    }

    public int Count()
    {
        return _users.Count;
    }

    public Result<User>? FindUser(string name)
    {
        return _users.FindBy(name, (result, name) => result.Name == name);
    }

    public IMyCollection<User> GetAllUsers()
    {
        return _users;
    }

    public bool RemoveUser(string name)
    {
        if(string.IsNullOrWhiteSpace(name)) return false;
        var result = FindUser(name);
        if (result.Succes)
        {
            _users.Remove(result.Value);
        }
        else
        {
            return false;
        }
        _repository.SaveUsers(_users);
        return true;
    }
}