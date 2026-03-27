using Project_1.Collections;

namespace Project_1.Repository;

public interface IUserRepository
{
    public IMyCollection<Developer> LoadAllUsers();
    public void SaveUsers(IMyCollection<Developer> users);
}