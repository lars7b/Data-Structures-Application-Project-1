using Project_1.Collections;

namespace Project_1.Service;

public interface IUserService
{
    IMyCollection<User> GetAllUsers();
    public int Count();
    public void AddUser(string name, string password);
    public User? FindUser(string name);
    public bool RemoveUser(string name);
}