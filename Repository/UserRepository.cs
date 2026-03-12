using Project_1.Collections;
using Project_1.Repository;
using Project_1.Model;
using System.Text.Json;
namespace Project_1.Repository;

public class UserRepository(string filePath) : IUserRepository
{
    public MyArray<Developer> LoadAllUsers()
    {
        if(!File.Exists(filePath)) return new MyArray<Developer>();

        var json = File.ReadAllText(filePath);
        var items = JsonSerializer.Deserialize<Developer[]>(json);

        var users = new MyArray<Developer>();
        if(items != null)
        {
            foreach(var item in items)
            {
                users.Add(item);
            }
        }
        return users;
    }

    public void SaveUsers(MyArray<Developer> users)
    {
        var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true});
        File.WriteAllText(filePath, json);
    }
}