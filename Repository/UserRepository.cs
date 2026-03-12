using Project_1.Collections;
using Project_1.Repository;
using Project_1.Model;
namespace Project_1.Repository;

public class UserRepository(string filePath) : IUserRepository
{
    public int Count()
    {
        throw new NotImplementedException();
    }

    public MyArray<IUser> LoadAllUsers()
    {
        throw new NotImplementedException();
    }

    public void SaveUsers(MyArray<IUser> users)
    {
        throw new NotImplementedException();
    }
}