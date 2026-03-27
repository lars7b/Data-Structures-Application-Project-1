using System.Text.Json;
using Project_1.Collections;

namespace Project_1.Repository;

public class UserRepository : IUserRepository
{
    private readonly string _filePath;
    private readonly IMyCollection<Developer> _users;

    public UserRepository(string filePath, IMyCollection<Developer> users)
    {
        _filePath = filePath;
        _users = users;
    }

    public IMyCollection<Developer> LoadAllUsers()
    {
        if (!File.Exists(_filePath)) return _users;

        var json = File.ReadAllText(_filePath);
        var items = JsonSerializer.Deserialize<Developer[]>(json);

        if (items != null)
            foreach (var item in items)
                _users.Add(item);
        return _users;
    }

    public void SaveUsers(IMyCollection<Developer> users)
    {
        var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}