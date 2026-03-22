using System.Text.Json;
using Project_1.Collections;

namespace Project_1.Repository;

public class UserRepository : IUserRepository
{
    private readonly string _filePath;

    public UserRepository(string filePath)
    {
        _filePath = filePath;
    }

    public MyArray<User> LoadAllUsers()
    {
        if (!File.Exists(_filePath)) File.Create(_filePath);

        var json = File.ReadAllText(_filePath);
        var items = JsonSerializer.Deserialize<User[]>(json);

        var users = new MyArray<User>();
        if (items != null)
            foreach (var item in items)
                users.Add(item);

        return users;
    }

    public void SaveUsers(MyArray<User> users)
    {
        var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}