using Project_1.Collections;
using Project_1.Model;

namespace Project_1.Service;

public interface IUserService
{
    IMyCollection<User> GetAllUsers();
    int Count();
    bool AddUser(string name, string password);
    Result<User> FindUser(string name);
    bool RemoveUser(string name);
}