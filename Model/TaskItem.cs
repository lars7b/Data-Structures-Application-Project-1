namespace Project_1.Model;

public class TaskItem
{
    public int Id { get; init; }
    public required string Description { get; init; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public string? AssignedTo { get; set; }

    public override string ToString()
    {
        var assignee = AssignedTo ?? "None";
        return $"[{Id}] *{assignee}* ({Priority}) {Description} |{CreatedAt.ToShortDateString()}|";
    }
}