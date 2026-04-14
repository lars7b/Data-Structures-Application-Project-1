using Microsoft.Extensions.DependencyInjection;
using Project_1.Repository;
using Project_1.Service;
using Project_1.View;
using Project_1.Collections;
using Project_1.Model;

namespace Project_1;

internal static class Program
{
    private static void Main(string[] args)
    {
        string collectionType = args.Length > 0 ? args[0].ToLower() : "array";

        var serviceCollection = new ServiceCollection();

        switch (collectionType)
        {
            case "linkedlist":
                serviceCollection.AddSingleton(typeof(IMyCollection<>), typeof(MyLinkedList<>));
                break;
            case "bst":
                serviceCollection.AddSingleton(typeof(IMyCollection<>), typeof(MyBinarySearchTree<>));
                break;
            default:
                serviceCollection.AddSingleton(typeof(IMyCollection<>), typeof(MyArray<>));
                break;
        }



        serviceCollection.AddSingleton<ITaskRepository>(provider =>
        {
            var taskCollection = provider.GetRequiredService<IMyCollection<TaskItem>>();
            return new JsonTaskRepository("tasks.json", taskCollection);

        });

        serviceCollection.AddSingleton<IUserRepository>(provider =>
        {
            var userCollection = provider.GetRequiredService<IMyCollection<User>>();
            return new UserRepository("user.json", userCollection);
        });

        serviceCollection.AddSingleton<ITaskService, TaskService>();
        serviceCollection.AddSingleton<IUserService, UserService>();
        serviceCollection.AddSingleton<ITaskView, KanbanTaskView>();
        serviceCollection.AddSingleton<ILoginService, LoginService>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var loginservice = serviceProvider.GetRequiredService<ILoginService>();
        var view = serviceProvider.GetRequiredService<ITaskView>();

        view.Run();
    }
}
