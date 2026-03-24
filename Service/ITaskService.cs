using Project_1.Collections;
using Project_1.Model;

namespace Project_1.Service;

public interface ITaskService
{
    IMyCollection<TaskItem> GetAllTasks();
    IMyCollection<TaskItem> GetTasksByPriority(Priority priority);
    IMyCollection<TaskItem> GetTasksByStatus(Status status);
    IMyCollection<TaskItem> GetTasksByDateCreated(DateTime date);
    void AddTask(string description, Priority priority = Priority.None, Status status = Status.Todo);
    public void AssignTaskToUser(int id, IUser user);
    void RemoveTask(int id);
    public void RemoveUserFromTask(int id);
    void ToggleStatus(int id);
    void ChangePriority(int id, Priority priority);
}