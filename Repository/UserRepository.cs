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

    public MyArray<Developer> LoadAllUsers()
    {
        if (!File.Exists(_filePath)) File.Create(_filePath);

        var json = File.ReadAllText(_filePath);
        var items = JsonSerializer.Deserialize<Developer[]>(json);

        var users = new MyArray<Developer>();
        if (items != null)
            foreach (var item in items)
                users.Add(item);

        return users;
    }

    public void SaveUsers(MyArray<Developer> users)
    {
        var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}