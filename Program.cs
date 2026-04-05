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
        
        if (collectionType == "linkedlist") serviceCollection.AddSingleton(typeof(IMyCollection<>), typeof(MyLinkedList<>));
        else serviceCollection.AddSingleton(typeof(IMyCollection<>), typeof(MyArray<>));

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