using Project_1.Collections;
using Project_1.Model;

namespace Project_1.Repository;

public interface ITaskRepository
{
    MyArray<TaskItem> LoadTasks();
    void SaveTasks(MyArray<TaskItem> tasks);
}