using Project_1.Collections;
using Project_1.Model;
using Project_1.Service;

namespace Project_1.View;

public class ConsoleTaskView(ITaskService service1, IUserService service2) : ITaskView
{
    public void Run()
    {
        while (true)
        {
            DisplayTasks(service1.GetAllTasks(), service2.GetAllUsers());
            Console.WriteLine($"Hello, {service2.LoggedInUser?.Name}");
            if(service2.LoggedInUser?.Name == "Admin")
            {
                Console.WriteLine("a. Add User");
                Console.WriteLine("r. Remove User");
            }
            Console.WriteLine("1. Sign up/Login");
            Console.WriteLine("\nOptions:");

            if (service2.LoggedIn == true)
            {
                Console.WriteLine("2. Add Task");
                Console.WriteLine("3. Remove Task");
                Console.WriteLine("4. Toggle Task State");
                Console.WriteLine("5. Exit");
            }

            var option = Prompt("Select an option: ");
            switch (option)
            {
                case "a":
                    if(service2.LoggedInUser?.Name == "Admin")
                    {
                        var name = Prompt("Enter a Name");
                        if (name != null) service2.AddUser(name);
                    }
                    break;
                case "r":
                    if(service2.LoggedInUser?.Name == "Admin")
                    {
                        var name = Prompt("Enter a Name");
                        if (name != null) service2.RemoveUser(name);
                    }
                    break;
                case "1":
                    var login = Prompt("Enter a name: ");
                    if(login != null)
                    {
                        service2.LoginUser(login);
                    }
                    break;
                case "2":
                    var description = Prompt("Enter task description: ");
                    if (description != null) service1.AddTask(description);
                    break;
                case "3":
                    if (service2.LoggedInUser?.Name == "Admin")
                    {
                        var removeIdStr = Prompt("Enter task id to remove: ");
                        if (int.TryParse(removeIdStr, out var removeId)) service1.RemoveTask(removeId);
                    }
                    Console.WriteLine("You don't have the right to do this");
                    break;
                case "4":
                    var toggleIdStr = Prompt("Enter task id to toggle: ");
                    if (int.TryParse(toggleIdStr, out var toggleId)) service1.ToggleTaskComplete(toggleId);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // private static void DisplayTasks(IMyCollection<TaskItem> tasks)
    // {
    //     Console.Clear();
    //     Console.WriteLine("==== ToDo List ====");
    //     foreach (var task in tasks) Console.WriteLine($"{task}");
    // }
    private static void DisplayTasks(IMyCollection<TaskItem> tasks, IMyCollection<Developer> users)
    {
        Console.Clear();
        if (users.Count > 0)
        {
            Console.WriteLine("==== UsersList ====");
            foreach(var user in users)Console.WriteLine($"{user.Name}");
        }
        Console.WriteLine("==== ToDo List ====");
        foreach (var task in tasks) Console.WriteLine($"{task}");
    }

    private static string? Prompt(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}