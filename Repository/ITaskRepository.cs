using Project_1.Model;

namespace Project_1.Repository;

public interface ITaskRepository
{
    List<TaskItem> LoadTasks();
    void SaveTasks(List<TaskItem> tasks);
}