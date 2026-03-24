using Project_1.Collections;

namespace Project_1.Repository;

public interface IUserRepository
{
    MyArray<User> LoadAllUsers();
    User? GetById(int id);
    void Add(User user);
    void Update(User user);
    void Delete(User user);
    void SaveUsers(MyArray<User> users);
}