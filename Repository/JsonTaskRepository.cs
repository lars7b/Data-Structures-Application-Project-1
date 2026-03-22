using System.Data.SqlTypes;
using System.Text.Json;
using Project_1.Collections;
using Project_1.Model;

namespace Project_1.Repository;

public class JsonTaskRepository : ITaskRepository
{
    private readonly string _filePath;

    public JsonTaskRepository(string filePath)
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

    public MyArray<TaskItem> LoadTasks()
    {
        if (!File.Exists(_filePath)) File.WriteAllText(_filePath, "[]");

        var json = File.ReadAllText(_filePath);
        try
        {
            var items = JsonSerializer.Deserialize<TaskItem[]>(json) ?? Array.Empty<TaskItem>();
            var tasks = new MyArray<TaskItem>();
            if (items != null)
                foreach (var item in items)
                    tasks.Add(item);
            return tasks;
        }
        catch (JsonException e)
        {
            //logging
            Console.WriteLine($"InnerException: {e.InnerException}, Message: {e.Message}");
            return new MyArray<TaskItem>();
        }

    }

    public void SaveTasks(MyArray<TaskItem> tasks)
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}