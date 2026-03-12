using Project_1.Collections;

namespace Project_1.Repository;
public interface IUserRepository
{
    public int Count();
    public MyArray<IUser> LoadAllUsers();
    public void SaveUsers(MyArray<IUser> users);
}