using System.Text.Json;
using Project_1.Model;
using Project_1.Collections;

namespace Project_1.Repository;

public class JsonTaskRepository(string filePath) : ITaskRepository
{
    public MyArray<TaskItem> LoadTasks()
    {
        if (!File.Exists(filePath)) return new MyArray<TaskItem>();

        var json = File.ReadAllText(filePath);
        var tasks = JsonSerializer.Deserialize<MyArray<TaskItem>>(json);
        return tasks ?? new MyArray<TaskItem>();
    }

    public void SaveTasks(MyArray<TaskItem> tasks)
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }
}