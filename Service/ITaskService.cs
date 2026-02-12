using Project_1.Model;

namespace Project_1.Service;

public interface ITaskService
{
    IEnumerable<TaskItem> GetAllTasks();
    void AddTask(string description);
    void RemoveTask(int id);
    void ToggleTaskComplete(int id);
}