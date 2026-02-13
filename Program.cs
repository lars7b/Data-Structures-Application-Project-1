using Project_1.Repository;
using Project_1.Service;
using Project_1.View;

namespace Project_1;

internal static class Program
{
    private static void Main(string[] args)
    {
        const string filePath = "tasks.jsons";
        var repository = new JsonTaskRepository(filePath);
        var service = new TaskService(repository);
        var view = new ConsoleTaskView(service);

        view.Run();
    }
}