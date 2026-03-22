using Project_1.Collections;

namespace Project_1.Service;

public interface IUserService
{
    IMyCollection<User> GetAllUsers();
    int Count();
    void AddUser(string name);
    Result<User> FindUser(string name);
    bool RemoveUser(string name);
}