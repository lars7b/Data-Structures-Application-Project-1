using Project_1.Repository;
using Project_1.Service;
using Project_1.View;

namespace Project_1;

internal static class Program
{
    public static Admin admin = new Admin(0);
    private static void Main(string[] args)
    {
        const string filePath = "tasks.jsons";
        const string filePath1 = "users.jsons";
        var repository = new JsonTaskRepository(filePath);
        var repository1 = new UserRepository(filePath1);
        var service = new TaskService(repository);
        var service1 = new UserService(repository1);
        service1.AddUser(admin.Name);
        var view = new ConsoleTaskView(service, service1);

        view.Run();
    }
}