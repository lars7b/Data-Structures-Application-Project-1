using Project_1.Model;
using Project_1.Service;

namespace Project_1.View;

public class ConsoleTaskView(ITaskService service) : ITaskView
{
    public void Run()
    {
        while (true)
        {
            DisplayTasks(service.GetAllTasks());
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Remove Task");
            Console.WriteLine("3. Toggle Task State");
            Console.WriteLine("4. Exit");

            var option = Prompt("Select an option: ");
            switch (option)
            {
                case "1":
                    var description = Prompt("Enter task description: ");
                    if (description != null) service.AddTask(description);
                    break;
                case "2":
                    var removeIdStr = Prompt("Enter task id to remove: ");
                    if (int.TryParse(removeIdStr, out var removeId)) service.RemoveTask(removeId);
                    break;
                case "3":
                    var toggleIdStr = Prompt("Enter task id to toggle: ");
                    if (int.TryParse(toggleIdStr, out var toggleId)) service.ToggleTaskComplete(toggleId);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void DisplayTasks(IEnumerable<TaskItem> tasks)
    {
        Console.Clear();
        Console.WriteLine("==== ToDo List ====");
        foreach (var task in tasks) Console.WriteLine($"{task}");
    }

    private static string? Prompt(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}