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
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Sign up/Login");

            if (service2.LoggedInUser?.Name == "Admin")
            {
                Console.WriteLine("a. Add User");
                Console.WriteLine("b. Remove User");
                Console.WriteLine("c. Add User To Task");
                Console.WriteLine("d. Remove User From Task");
            }

            if (service2.LoggedInUser != null)
            {
                Console.WriteLine("2. Add Task");
                Console.WriteLine("3. Remove Task");
                Console.WriteLine("4. Toggle Task Stats");
                Console.WriteLine("5. Toggle Task Priority");
                Console.WriteLine("6. Filter or Sort Tasks");
            }

            Console.WriteLine("7. Exit");

            var option = Prompt("Select an option: ");
            switch (option)
            {
                case "a":
                    if (service2.LoggedInUser?.Name == "Admin")
                    {
                        var name = Prompt("Enter a Name: ");
                        if (name != null) service2.AddUser(name);
                    }

                    break;
                case "b":
                    if (service2.LoggedInUser?.Name == "Admin")
                    {
                        var name = Prompt("Enter a Name: ");
                        if (name != null) service2.RemoveUser(name);
                    }

                    break;
                case "c":
                    if (service2.LoggedInUser?.Name == "Admin")
                    {
                        var id = Prompt("Enter task id: ");
                        var name = Prompt("Enter name: ");
                        if (id != null && name != null)
                            if (int.TryParse(id, out var result))
                                service1.AssignTaskToUser(result, service2.FindUser(name));
                    }

                    break;
                case "d":
                    if (service2.LoggedInUser?.Name == "Admin")
                    {
                        var id = Prompt("Enter Id: ");
                        if (int.TryParse(id, out var result)) service1.RemoveUserFromTask(result);
                    }

                    break;
                case "1":
                    var login = Prompt("Enter a name: ");
                    if (login != null) service2.LoginUser(login);
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

                    // Console.WriteLine("You don't have the right to do this");
                    break;
                case "4":
                    var toggleIdStr = Prompt("Enter task id to toggle status: ");
                    if (int.TryParse(toggleIdStr, out var toggleId)) service1.ToggleStatus(toggleId);
                    break;
                case "5":
                    var changeIdStr = Prompt("Enter task id to change priority: ");
                    if (int.TryParse(changeIdStr, out var changeId))
                    {
                        var priorityStr = Prompt("What priority ");
                        if (Enum.TryParse<Priority>(priorityStr, out var priority))
                            service1.ChangePriority(changeId, priority);
                    }

                    break;
                case "6":
                    Console.WriteLine("\nOptions:");
                    Console.WriteLine("1. Filter by Priority");
                    Console.WriteLine("2. Filter by Status");
                    Console.WriteLine("3. Filter by Creation Date");

                    var selection = Prompt("Select an option: ");

                    switch (selection)
                    {
                        case "1":
                            var priorityStr = Prompt("Enter priority: ");
                            if (Enum.TryParse<Priority>(priorityStr, out var priority))
                                DisplayTasks(service1.GetTasksByPriority(priority), service2.GetAllUsers());
                            break;
                        case "2":
                            var statusStr = Prompt("Enter status: ");
                            if (statusStr == null) break;
                            if (Enum.TryParse<Status>(statusStr.Trim(), out var status))
                                DisplayTasks(service1.GetTasksByStatus(status), service2.GetAllUsers());
                            break;
                        case "3":
                            var dateStr = Prompt("Enter date (MM/dd/yyyy): ");
                            if (DateTime.TryParse(dateStr, out var date))
                                DisplayTasks(service1.GetTasksByDateCreated(date), service2.GetAllUsers());
                            else
                                Console.WriteLine("Invalid date.");
                            break;
                        default:
                            Console.WriteLine("Invalid option. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                case "7":
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
            foreach (var user in users) Console.WriteLine($"{user.Name}");
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