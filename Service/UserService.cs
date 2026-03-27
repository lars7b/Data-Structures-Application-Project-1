using Project_1.Collections;
using Project_1.Repository;

namespace Project_1.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMyCollection<Developer> _users;
    private bool _loggedIn;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
        _users = _repository.LoadAllUsers();
        var user = "Admin";
        var result = _users.FindBy(user, (u, key) => u.Name == key);
        if (result == null)
        {
            _users.Add(new Developer(user) { Rights = Authorization.Admin });
            _repository.SaveUsers(_users);
        }
        _loggedIn = false;
    }

    public Developer? LoggedInUser { get; set; }

    public bool LoggedIn
    {
        get => _loggedIn;
        set => _ = false;
    }

    public void AddUser(string name) 
    {
        var newId = 1;

        while (_users.FindBy(newId, (user, key) => user.Id == key) != null)
        {
            newId++;
        }

        var newUser = new Developer(name) { Id = newId, Name = name, Rights = Authorization.Dev };
        _users.Add(newUser);
        _repository.SaveUsers(_users);
    }

    public int Count()
    {
        return _users.Count;
    }

    public IMyCollection<Developer> GetAllUsers()
    {
        return _users;
    }

    public IUser? FindUser(string name)
    {
        var result = _users.FindBy(name, (user, key) => user.Name == key);
        return result?.Value;
    }

    public void RemoveUser(string name)
    {
        var user = _users.FindBy(name, (user, key) => user.Name == key);
        if (user == null) return;
        _users.Remove(user.Value);
        _repository.SaveUsers(_users);
    }

    public void LoginUser(string name)
    {
        var user = _users.FindBy(name, (user, key) => user.Name == key);
        if (user == null) return;
        LoggedInUser = user?.Value;
        _loggedIn = !_loggedIn;
    }
}