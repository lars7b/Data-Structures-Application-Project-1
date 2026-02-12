using Project_1.Model;
using Project_1.Repository;

namespace Project_1.Service;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly List<TaskItem> _tasks;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
        _tasks = _repository.LoadTasks();
    }

    public IEnumerable<TaskItem> GetAllTasks()
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

    public void RemoveTask(int id)
    {
        var task = _tasks.Find(x => x.Id == id);
        if (task == null) return;
        _tasks.Remove(task);
        _repository.SaveTasks(_tasks);
    }

    public void ToggleTaskComplete(int id)
    {
        var task = _tasks.Find(x => x.Id == id);
        if (task == null) return;
        task.Completed = !task.Completed;
        _repository.SaveTasks(_tasks);
    }
}