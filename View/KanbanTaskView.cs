using Project_1.Collections;
using Project_1.Model;
using Project_1.Service;
using Spectre.Console;
using Status = Project_1.Model.Status;

namespace Project_1.View;

public class KanbanTaskView(ITaskService taskService, IUserService userService) : ITaskView
{
    private IMyCollection<TaskItem> _todo = taskService.GetTasksByStatus(Status.Todo);

    public void Run()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Text("Kanban Task Board", Color.Blue).Centered());
        AnsiConsole.WriteLine();

        var todo = taskService.GetTasksByStatus(Status.Todo);
        var inProgress = taskService.GetTasksByStatus(Status.InProgress);
        var done = taskService.GetTasksByStatus(Status.Done);

        var table = new Table()
            .Border(TableBorder.HeavyHead)
            .Expand()
            .BorderColor(Color.Gray)
            .AddColumn("[bold darkred_1]Todo[/]")
            .AddColumn("[bold olive]Doing[/]")
            .AddColumn("[bold darkgreen]Done[/]");

        var todoList = ToStringArray(todo);
        var inProgressList = ToStringArray(inProgress);
        var doneList = ToStringArray(done);

        var maxRows = Math.Max(todoList.Length, Math.Max(inProgressList.Length, doneList.Length));

        for (var i = 0; i < maxRows; i++)
            table.AddRow(
                i < todoList.Length ? todoList[i] : string.Empty,
                i < inProgressList.Length ? inProgressList[i] : string.Empty,
                i < doneList.Length ? doneList[i] : string.Empty
            );

        AnsiConsole.Write(table);
        AnsiConsole.Write(
            new Markup(
                    "Tasks have the following Structure:          [blue][[ID]][/] [orange3](Priority)[/] [navajowhite1]Description[/] [red]|Date|[/]")
                .Centered());
    }

    private static string[] ToStringArray(IMyCollection<TaskItem> collection)
    {
        var result = new string[collection.Count];
        var i = 0;
        foreach (var item in collection) result[i++] = Markup.Escape(item.ToString() ?? string.Empty);
        return result;
    }
}