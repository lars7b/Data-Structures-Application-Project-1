using System.Text.Json;
using Project_1.Collections;
using Project_1.Model;

namespace Project_1.Repository;

public class JsonTaskRepository : ITaskRepository
{
    private readonly string _filePath;
    private readonly IMyCollection<TaskItem> _tasks;

    public JsonTaskRepository(string filePath, IMyCollection<TaskItem> tasks)
    {
        _filePath = filePath;
        _tasks = tasks;
    }

    public IMyCollection<TaskItem> LoadTasks()
    {
        if (!File.Exists(_filePath)) return _tasks;

        var json = File.ReadAllText(_filePath);
        var items = JsonSerializer.Deserialize<TaskItem[]>(json);

        if (items != null)
            foreach (var item in items)
                _tasks.Add(item);
        return _tasks;
    }

    public void SaveTasks(IMyCollection<TaskItem> tasks)
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}