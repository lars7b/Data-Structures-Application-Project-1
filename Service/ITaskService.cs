using Project_1.Collections;
using Project_1.Model;

namespace Project_1.Service;

public interface ITaskService
{
    IMyCollection<TaskItem> GetAllTasks();
    void AddTask(string description);
    void RemoveTask(int id);
    void ToggleTaskComplete(int id);
}