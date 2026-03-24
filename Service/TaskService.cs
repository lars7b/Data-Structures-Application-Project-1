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

    public IMyCollection<TaskItem> GetTasksByPriority(Priority priority)
    {
        return _tasks.Filter(x => x.Priority == priority);
    }

    public IMyCollection<TaskItem> GetTasksByStatus(Status status)
    {
        return _tasks.Filter(x => x.Status == status);
    }

    public IMyCollection<TaskItem> GetTasksByDateCreated(DateTime date)
    {
        return _tasks.Filter(x => x.CreatedAt.Date == date.Date);
    }

    public void AssignTaskToUser(int id, string username)
    {
        var taskItem = _tasks.FindBy(id, (task, key) => task.Id == key)?.Value;
        if (taskItem == null) return;
        taskItem.AssignedTo = username;
        _repository.SaveTasks(_tasks);
    }

    public void RemoveUserFromTask(int id)
    {
        var taskItem = _tasks.FindBy(id, (task, key) => task.Id == key)?.Value;
        if (taskItem == null) return;
        taskItem.AssignedTo = null;
        _repository.SaveTasks(_tasks);
    }

    public void AddTask(string description, string username, Priority priority = Priority.None, Status status = Status.Todo)
    {
        var newId = _tasks.Count > 0 ? _tasks[^1].Id + 1 : 1;
        var newTask = new TaskItem { Id = newId, Description = description, AssignedTo= username, Priority = priority, Status = status };
        _tasks.Add(newTask);
        _repository.SaveTasks(_tasks);
    }

    public void RemoveTask(int id)
    {
        var task = _tasks.FindBy(id, (task, key) => task.Id == key);
        if (task == null) return;
        _tasks.Remove(task.Value);
        _repository.SaveTasks(_tasks);
    }

    public void ToggleStatus(int id)
    {
        var task = _tasks.FindBy(id, (task, key) => task.Id == key);
        if (task == null) return;
        task.Value.Status = task.Value.Status switch
        {
            Status.Todo => Status.Doing,
            Status.Doing => Status.Done,
            Status.Done => Status.Doing,
            _ => Status.Todo
        };

        _repository.SaveTasks(_tasks);
    }

    public void ChangePriority(int id, Priority priority)
    {
        var task = _tasks.FindBy(id, (task, key) => task.Id == key);
        if (task == null) return;
        task.Value.Priority = priority;
        _repository.SaveTasks(_tasks);
    }
}