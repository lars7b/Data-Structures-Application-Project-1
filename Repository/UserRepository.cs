using System.Text.Json;
using Project_1.Collections;

namespace Project_1.Repository;

public class UserRepository : IUserRepository
{
    private readonly string _filePath;

    public UserRepository(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("Invalid file path");
        }
        string? directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }
        _filePath = filePath;
    }

    public void Add(User user)
    {
        throw new NotImplementedException();
    }

    public void Delete(User user)
    {
        throw new NotImplementedException();
    }

    public User? GetById(int id)
    {
        throw new NotImplementedException();
    }
    public User? GetByName(string name)
    {
        var users = LoadAllUsers().ToArray();
        var find = users.FirstOrDefault(_=>_.Name == name);
        return find;
    }

    public MyArray<User> LoadAllUsers()
    {
        if (!File.Exists(_filePath)) File.WriteAllText(_filePath, "[]");

        var json = File.ReadAllText(_filePath);
        try
        {
            var items = JsonSerializer.Deserialize<User[]>(json) ?? Array.Empty<User>();
            var users = new MyArray<User>();
            if (items != null)
                foreach (var item in items)
                    users.Add(item);
            return users;
        }
        catch (JsonException e)
        {
            // logging
            Console.WriteLine($"InnerException: {e.InnerException}, Message: {e.Message}");
            return new MyArray<User>();
        }

    }

    public void SaveUsers(MyArray<User> users)
    {
        var json = JsonSerializer.Serialize(users.ToArray(), new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }
}