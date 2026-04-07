using Project_1.Collections;
using Project_1.Model;

namespace Project_1.Repository;

public interface IUserRepository
{
    IMyCollection<User> LoadAllUsers();
    void SaveUsers(IMyCollection<User> users);
    Result<User> GetUserByUsername(string username);
}