using Project_1.Collections;

namespace Project_1.Repository;

public interface IUserRepository
{
    public MyArray<User> LoadAllUsers();
    public void SaveUsers(MyArray<User> users);
}