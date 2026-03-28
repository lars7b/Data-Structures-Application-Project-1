using Project_1.Collections;

namespace Project_1.Service;

public interface IUserService
{
    IMyCollection<User> GetAllUsers();
    public int Count();
    public bool AddUser(string name, string password);
    public Result<User?> FindUser(string name);
    public bool RemoveUser(string name);
}