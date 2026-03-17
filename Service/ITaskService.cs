using Project_1.Collections;
using Project_1.Model;

namespace Project_1.Service;

public interface ITaskService
{
    IMyCollection<TaskItem> GetAllTasks();
    void AddTask(string description);
    public void AssignTaskToUser(int id, IUser user);
    void RemoveTask(int id);
    public void RemoveUserFromTask(int id);
    void ToggleTaskComplete(int id);
}