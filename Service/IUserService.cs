using Project_1.Collections;

namespace Project_1.Service;
public interface IUserService
{
    IMyCollection<Developer> GetAllUsers();
    public int Count();
    public void AddUser(string name);
    public void RemoveUser(int id);
}