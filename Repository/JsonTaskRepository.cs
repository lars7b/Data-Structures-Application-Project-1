using System.Text.Json;
using Project_1.Collections;
using Project_1.Model;

namespace Project_1.Repository;

public class JsonTaskRepository : ITaskRepository
{
    private readonly string _filePath;

    public JsonTaskRepository(string filePath)
    {
        _filePath = filePath;
    }

    public MyArray<TaskItem> LoadTasks()
    {
        if (!File.Exists(_filePath)) return new MyArray<TaskItem>();

        var json = File.ReadAllText(_filePath);
        var items = JsonSerializer.Deserialize<TaskItem[]>(json);

        var tasks = new MyArray<TaskItem>();
        if (items != null)
            foreach (var item in items)
                tasks.Add(item);
        return tasks;
    }

    public void SaveTasks(MyArray<TaskItem> tasks)
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}