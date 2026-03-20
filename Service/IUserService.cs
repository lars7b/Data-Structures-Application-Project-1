using Project_1.Collections;

namespace Project_1.Service;

public interface IUserService
{
    public bool LoggedIn { get; set; }
    public Developer? LoggedInUser { get; set; }
    IMyCollection<Developer> GetAllUsers();
    public int Count();
    public void AddUser(string name);
    public IUser? FindUser(string name);
    public void RemoveUser(string name);
    public void LoginUser(string name);
}