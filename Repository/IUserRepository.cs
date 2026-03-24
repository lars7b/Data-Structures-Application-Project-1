using Project_1.Collections;

namespace Project_1.Repository;

public interface IUserRepository
{
    public MyArray<Developer> LoadAllUsers();
    public void SaveUsers(MyArray<Developer> users);
}