using Project_1.Repository;
using Project_1.Service;
using Project_1.View;

namespace Project_1;

internal static class Program
{
    private static void Main(string[] args)
    {
        const string filePath = "tasks.json";
        const string filePath1 = "users.json";
        var repository = new JsonTaskRepository(filePath);
        var repository1 = new UserRepository(filePath1);
        var service = new TaskService(repository);
        var service1 = new UserService(repository1);
        var view = new KanbanTaskView(service, service1);

        view.Run();
    }
}