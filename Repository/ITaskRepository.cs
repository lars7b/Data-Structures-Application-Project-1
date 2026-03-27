using Project_1.Collections;
using Project_1.Model;

namespace Project_1.Repository;

public interface ITaskRepository
{
    IMyCollection<TaskItem> LoadTasks();
    void SaveTasks(IMyCollection<TaskItem> tasks);
}