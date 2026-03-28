using Project_1.Collections;

namespace Project_1.Repository;

public interface IUserRepository
{
    public IMyCollection<User> LoadAllUsers();
    public void SaveUsers(IMyCollection<User> users);
}