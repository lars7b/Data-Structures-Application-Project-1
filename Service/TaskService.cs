using Project_1.Collections;
using Project_1.Model;
using Project_1.Repository;

namespace Project_1.Service;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly MyArray<TaskItem> _tasks;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
        _tasks = _repository.LoadTasks();
    }

    public IMyCollection<TaskItem> GetAllTasks()
    {
        return _tasks;
    }

    public void AddTask(string description)
    {
        var newId = _tasks.Count > 0 ? _tasks[^1].Id + 1 : 1;
        var newTask = new TaskItem { Id = newId, Description = description, Completed = false };
        _tasks.Add(newTask);
        _repository.SaveTasks(_tasks);
    }

    public void AssignTaskToUser(int id, IUser user)
    {
        var taskItem = _tasks.FindBy(id, (task, key) => task.Id == key)?.Value;
        if (taskItem == null) return;
        taskItem.AssignedTo = user.Name;
        _repository.SaveTasks(_tasks);
    }

    public void RemoveUserFromTask(int id)
    {
        var taskItem = _tasks.FindBy(id, (task, key) => task.Id == key)?.Value;
        if (taskItem == null) return;
        taskItem.AssignedTo = null;
        _repository.SaveTasks(_tasks);
    }
    public void RemoveTask(int id)
    {
        var task = _tasks.FindBy(id, (task, key) => task.Id == key);
        if (task == null) return;
        _tasks.Remove(task.Value);
        _repository.SaveTasks(_tasks);
    }

    public void ToggleTaskComplete(int id)
    {
        var task = _tasks.FindBy(id, (task, key) => task.Id == key);
        if (task == null) return;
        task.Value.Completed = !task.Value.Completed;
        _repository.SaveTasks(_tasks);
    }
}