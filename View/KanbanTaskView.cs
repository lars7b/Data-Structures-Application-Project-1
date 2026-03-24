using Project_1.Collections;
using Project_1.Model;
using Project_1.Service;
using Spectre.Console;
using Status = Project_1.Model.Status;

namespace Project_1.View;

public class KanbanTaskView(ITaskService taskService, IUserService userService) : ITaskView
{
    public void Run()
    {
        while (true)
        {
            DrawTable();

            var isLoggedIn = userService.LoggedInUser != null;
            var isAdmin = userService.LoggedInUser?.Name == "Admin";

            var choices = new List<string> { "Signup/Login" };

            if (isLoggedIn)
            {
                choices.Remove("Signup/Login");
                if (isAdmin)
                {
                    choices.Add("Add User");
                    choices.Add("Remove User");
                    choices.Add("Assign User to Task");
                    choices.Add("Remove User from Task");
                }

                choices.Add("Add Task");

                if (isAdmin)
                    choices.Add("Remove Tasks");

                choices.Add("Toggle Task State");
                choices.Add("Filter or Sort Task List");
            }

            choices.Add("Exit");

            var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().Title(
                    $"Logged in as: [bold]{(isLoggedIn ? userService.LoggedInUser!.Name : "Guest")}[/] - Select an option:")
                .AddChoices(choices));

            switch (choice)
            {
                case "Signup/Login":
                    var name = AnsiConsole.Ask<string>("Enter your name: ");
                    userService.LoginUser(name);
                    break;

                case "Add User":
                    var userToAdd = AnsiConsole.Ask<string>("Enter new user name: ");
                    userService.AddUser(userToAdd);
                    break;

                case "Remove User":
                    var userToRemove = AnsiConsole.Ask<string>("Enter user to remove: ");
                    userService.RemoveUser(userToRemove);
                    break;

                case "Assign User to Task":
                    var taskIdToAssign = AnsiConsole.Ask<int>("Enter task id: ");
                    var usernameToAssign =
                        AnsiConsole.Ask<string>($"Enter user name to assign to task: {taskIdToAssign}");
                    var userToAssign = userService.FindUser(usernameToAssign);
                    if (userToAssign != null)
                        taskService.AssignTaskToUser(taskIdToAssign, userToAssign);
                    break;

                case "Remove User from Task":
                    var taskIdToRemoveUser = AnsiConsole.Ask<int>("Enter task id: ");
                    taskService.RemoveUserFromTask(taskIdToRemoveUser);
                    break;

                case "Add Task":
                    var description = AnsiConsole.Ask<string>("Enter task description: ");
                    taskService.AddTask(description);
                    break;

                case "Remove Task":
                    var taskToRemove = AnsiConsole.Ask<int>("Enter task id to remove: ");
                    taskService.RemoveTask(taskToRemove);
                    break;

                case "Toggle Task State":
                    var idToToggle = AnsiConsole.Ask<int>("Enter task id: ");
                    var state = AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title("Select which state to change/toggle: ").AddChoices("Status", "Priority"));
                    switch (state)
                    {
                        case "Status":
                            taskService.ToggleStatus(idToToggle);
                            break;
                        case "Priority":
                            var priority = AnsiConsole.Prompt(new SelectionPrompt<Priority>().AddChoices(Priority.None,
                                Priority.Low, Priority.Normal, Priority.High, Priority.Critical));
                            taskService.ChangePriority(idToToggle, priority);
                            break;
                    }

                    break;

                case "Filter or Sort Task List":
                    IMyCollection<TaskItem> filtered;
                    var filterType =
                        AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Filter by")
                            .AddChoices("Status", "Priority", "Date"));
                    switch (filterType)
                    {
                        case "Status":
                            var statusFilter = AnsiConsole.Prompt(
                                new SelectionPrompt<Status>().AddChoices(Status.Todo, Status.Doing, Status.Done));
                            filtered = taskService.GetTasksByStatus(statusFilter);
                            AnsiConsole.Clear();
                            AnsiConsole.Write(CreateTable(filtered));
                            AnsiConsole.Write("\nPress any key to continue...");
                            Console.ReadKey();
                            break;
                        case "Priority":
                            var priorityFilter = AnsiConsole.Prompt(
                                new SelectionPrompt<Priority>().AddChoices(Priority.None, Priority.Low, Priority.Normal,
                                    Priority.High, Priority.Critical));
                            filtered = taskService.GetTasksByPriority(priorityFilter);
                            AnsiConsole.Clear();
                            AnsiConsole.Write(CreateTable(filtered));
                            AnsiConsole.Write("\nPress any key to continue...");
                            Console.ReadKey();
                            break;
                        case "Date":
                            var dateFilter = AnsiConsole.Ask<string>("Enter date (MM/DD/YYYY): ");
                            if (!DateTime.TryParse(dateFilter, out var date))
                                AnsiConsole.MarkupLine("[red]Invalid date.[/]");
                            filtered = taskService.GetTasksByDateCreated(date);
                            AnsiConsole.Clear();
                            AnsiConsole.Write(CreateTable(filtered));
                            AnsiConsole.Write("\nPress any key to continue...");
                            Console.ReadKey();
                            break;
                    }

                    break;

                case "Exit":
                    return;
            }
        }
    }

    private void DrawTable()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(CreatePanel(taskService.GetAllTasks()));
        AnsiConsole.Write(
            new Markup(
                    "Tasks have the following Structure: [blue][[ID]][/] [purple]*Assignee*[/] [orange3](Priority)[/] [navajowhite1]Description[/] [red]|Date|[/]")
                .Centered());
    }

    private static Panel CreatePanel(IMyCollection<TaskItem> tasks)
    {
        return new Panel(CreateTable(tasks)).Header("[blue bold]Kanban Task Board[/]").RoundedBorder()
            .Expand();
    }

    private static Table CreateTable(IMyCollection<TaskItem> tasks)
    {
        var table = new Table()
            .Border(TableBorder.HeavyHead)
            .Expand()
            .BorderColor(Color.Gray)
            .AddColumn("[bold darkred_1]Todo[/]")
            .AddColumn("[bold olive]Doing[/]")
            .AddColumn("[bold darkgreen]Done[/]");

        var todoRows = (MyArray<TaskItem>)tasks.Filter(x => x.Status == Status.Todo);
        var doingRows = (MyArray<TaskItem>)tasks.Filter(x => x.Status == Status.Doing);
        var doneRows = (MyArray<TaskItem>)tasks.Filter(x => x.Status == Status.Done);

        var maxRows = Math.Max(todoRows.Count, Math.Max(doingRows.Count, doneRows.Count));

        for (var i = 0; i < maxRows; i++)
            table.AddRow(
                i < todoRows.Count ? ToMarkUp(todoRows[i]) : string.Empty,
                i < doingRows.Count ? ToMarkUp(doingRows[i]) : string.Empty,
                i < doneRows.Count ? ToMarkUp(doneRows[i]) : string.Empty);

        return table;
    }

    private static string ToMarkUp(TaskItem item)
    {
        var assignee = item.AssignedTo != null ? $" *{Markup.Escape(item.AssignedTo)}*" : string.Empty;
        return
            $"[[{item.Id}]]{assignee} ({item.Priority}) {Markup.Escape(item.Description)} |{item.CreatedAt.ToShortDateString()}|";
    }
}