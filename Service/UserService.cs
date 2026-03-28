using Project_1.Collections;
using Project_1.Repository;

namespace Project_1.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMyCollection<User> _users;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
        _users = _repository.LoadAllUsers();
    }

    public void AddUser(string name, string password)
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        throw new NotImplementedException();
    }

    public User? FindUser(string name)
    {
        throw new NotImplementedException();
    }

    public IMyCollection<User> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public bool RemoveUser(string name)
    {
        throw new NotImplementedException();
    }
}