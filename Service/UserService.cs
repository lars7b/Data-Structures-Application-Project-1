using Project_1.Collections;
using Project_1.Model;
using Project_1.Repository;

namespace Project_1.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly MyArray<Developer> _users;
    public void AddUser(string name)
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        throw new NotImplementedException();
    }

    public IMyCollection<Developer> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public void RemoveUser(int id)
    {
        throw new NotImplementedException();
    }
}