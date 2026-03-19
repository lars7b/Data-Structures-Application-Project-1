using Project_1.Collections;
using Project_1.Repository;

namespace Project_1.Service;

public class UserService : IUserService<IUser>
{
    private readonly IUserRepository _repository;
    private readonly MyArray<Developer> _users;
    private bool _loggedIn;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
        _users = _repository.LoadAllUsers();
        _loggedIn = false;
    }

    public Developer? LoggedInUser { get; set; }

    public bool LoggedIn
    {
        get => _loggedIn;
        set => _ = false;
    }

    public void AddUser(string name, IUser who)
    {
        if(!(who.Acces == Authorization.Admin)) return;
        var newId = Count() > 0 ? _users[^1].Id + 1 : 1;
        var newUser = new Developer(name) { Id = newId, Name = name};
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

    public Result<IUser> FindUser(string name)
    {
        var user = _users.FindBy(name, (user, key) => user.Name == key)?.Value;
        if (user != null) return new Result<IUser>(true, user);
        return new Result<IUser>(false, null);
    }

    public void RemoveUser(string name, IUser who)
    {
        if(!(who.Acces == Authorization.Admin)) return;
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
public record Result<T>(bool result, T Value);