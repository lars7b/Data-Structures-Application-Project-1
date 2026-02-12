using System.Text.Json;
using Project_1.Model;

namespace Project_1.Repository;

public class JsonTaskRepository(string filePath) : ITaskRepository
{
    public List<TaskItem> LoadTasks()
    {
        if (!File.Exists(filePath)) return new List<TaskItem>();

        var json = File.ReadAllText(filePath);
        var tasks = JsonSerializer.Deserialize<List<TaskItem>>(json);
        return tasks ?? new List<TaskItem>();
    }

    public void SaveTasks(List<TaskItem> tasks)
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }
}