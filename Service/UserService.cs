using Project_1.Collections;
using Project_1.Model;
using Project_1.Repository;

namespace Project_1.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly MyArray<Developer> _users;
    public UserService(IUserRepository repository)
    {
        _repository = repository;
        _users = _repository.LoadAllUsers();
    }
    public void AddUser(string name)
    {
        var newId = Count() > 0 ? _users[^1].Id + 1 : 1;
        var newUser =  new Developer(name) { Id = newId, Name = name, Rights = Authorization.Dev};
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

    public void RemoveUser(int id)
    {
        var user = _users.FindBy(id, (user, key) => user.Id == key);
        if(user == null) return;
        _users.Remove(user.Value);
        _repository.SaveUsers(_users);
    }
}