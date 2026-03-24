using Project_1.Collections;
using Project_1.Model;
using Project_1.Service;

namespace Project_1.View;

public class ConsoleTaskView(ITaskService service1, IUserService service2, ILoginService service3) : ITaskView
{
    public void Run()
    {
        while (true)
        {
            DisplayTasks(service1.GetAllTasks(), service2.GetAllUsers());
            Console.WriteLine("\nOptions:");
            if (!service3.LoggedIn)
            {
                Console.WriteLine("1. Sign up/Login");
            }
            else
            {
                Console.WriteLine("1.logout");
            }

            if (service3.LoggedIn && service3.IsAdmin)
            {
                Console.WriteLine("a. Add User");
                Console.WriteLine("b. Remove User");
                Console.WriteLine("c. Assign Task To Different User");
                Console.WriteLine("d. Remove User From Task");
            }

            if(service3.LoggedIn)
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
                    var name = Prompt("Enter a Name: ");
                    var password = Prompt("Enter a Password");
                    if (name != null) service2.AddUser(name, password);
                    break;
                    
                case "b":
                    name = Prompt("Enter a Name: ");
                    if (name != null) service2.RemoveUser(name);
                    break;

                case "c":
                    var id = Prompt("Enter task id: ");
                    name = Prompt("Enter name: ");
                    if (id != null && name != null)
                    {
                        var found = service2.FindUser(name);
                        if(found.isSucces == false) break;
                        if (int.TryParse(id, out var result1 )) service1.AssignTaskToUser(result1, found.Value.Name);
                        break;
                    }
                    break;
                
                case "d":
                    id = Prompt("Enter Id: ");
                    if (int.TryParse(id, out var result)) service1.RemoveUserFromTask(result);

                    break;
                case "1":
                    if (service3.LoggedIn)
                    {
                        service3.Logout();
                        break;
                    }
                    var login = Prompt("Enter a name: ");
                    password = Prompt("Enter a password");
                    if(login != null && password != null)
                        service3.Login(login, password);;
                    break;
                case "2":
                    var description = Prompt("Enter task description: ");
                    if (description != null) service1.AddTask(description, service3.Luser.Name);
                    break;
                case "3":
                    var removeIdStr = Prompt("Enter task id to remove: ");
                    if (int.TryParse(removeIdStr, out var removeId)) service1.RemoveTask(removeId);
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
    private static void DisplayTasks(IMyCollection<TaskItem> tasks, IMyCollection<User> users)
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