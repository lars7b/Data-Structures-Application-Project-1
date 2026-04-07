using Project_1.Collections;
using Project_1.Model;
using Project_1.Service;

namespace Project_1.View;

public class ConsoleTaskView(ITaskService taskservice, IUserService userservice, LoginService loginservice) : ITaskView
{
    private bool _loggedin = false;
    public void Run()
    {
        while (true)
        {
            DisplayTasks(taskservice.GetAllTasks(), userservice.GetAllUsers());
            if(_loggedin)
            {
                Console.WriteLine($"Hello, {loginservice.CurrentUser.Name}");
            }
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Sign up/Login");

            if (loginservice.CurrentUser.Role == Access.Admin)
            {
                Console.WriteLine("a. Add User");
                Console.WriteLine("b. Remove User");
                Console.WriteLine("c. Add User To Task");
                Console.WriteLine("d. Remove User From Task");
            }

            if (_loggedin)
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
                    if (_loggedin && loginservice.CheckRoles() == Access.Admin)
                    {
                        var name = Prompt("Enter a Name: ");
                        if (name != null) ;
                    }

                    break;
                case "b":
                    if (_loggedin && loginservice.CheckRoles() == Access.Admin)
                    {
                        var name = Prompt("Enter a Name: ");
                        if (name != null) userservice.RemoveUser(name);
                    }

                    break;
                case "c":
                    if (_loggedin && loginservice.CheckRoles() == Access.Admin)
                    {
                        var id = Prompt("Enter task id: ");
                        var name = Prompt("Enter name: ");
                        if (id != null && name != null)
                            if (int.TryParse(id, out var result))
                                taskservice.AssignTaskToUser(result, loginservice.CurrentUser);
                    }

                    break;
                case "d":
                    if (_loggedin && loginservice.CheckRoles() == Access.Admin)
                    {
                        var id = Prompt("Enter Id: ");
                        if (int.TryParse(id, out var result)) taskservice.RemoveUserFromTask(result);
                    }

                    break;
                case "1":
                    var login = Prompt("Enter a name: ");
                    if (login != null)
                    {
                        var succes = loginservice.Login(login);
                        if(succes) _loggedin = true;
                    }
                    break;
                case "2":
                    if (_loggedin)
                    {
                        var description = Prompt("Enter task description: ");
                        if (description != null) taskservice.AddTask(loginservice.CurrentUser.Name, description);
                    }
                    break;
                case "3":
                    if (_loggedin)
                    {
                        var removeIdStr = Prompt("Enter task id to remove: ");
                        if (int.TryParse(removeIdStr, out var removeId)) taskservice.RemoveTask(removeId);
                    }

                    // Console.WriteLine("You don't have the right to do this");
                    break;
                case "4":
                    var toggleIdStr = Prompt("Enter task id to toggle status: ");
                    if (int.TryParse(toggleIdStr, out var toggleId)) taskservice.ToggleStatus(toggleId);
                    break;
                case "5":
                    var changeIdStr = Prompt("Enter task id to change priority: ");
                    if (int.TryParse(changeIdStr, out var changeId))
                    {
                        var priorityStr = Prompt("What priority ");
                        if (Enum.TryParse<Priority>(priorityStr, out var priority))
                            taskservice.ChangePriority(changeId, priority);
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
                                DisplayTasks(taskservice.GetTasksByPriority(priority), userservice.GetAllUsers());
                            break;
                        case "2":
                            var statusStr = Prompt("Enter status: ");
                            if (statusStr == null) break;
                            if (Enum.TryParse<Status>(statusStr.Trim(), out var status))
                                DisplayTasks(taskservice.GetTasksByStatus(status), userservice.GetAllUsers());
                            break;
                        case "3":
                            var dateStr = Prompt("Enter date (MM/dd/yyyy): ");
                            if (DateTime.TryParse(dateStr, out var date))
                                DisplayTasks(taskservice.GetTasksByDateCreated(date), userservice.GetAllUsers());
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