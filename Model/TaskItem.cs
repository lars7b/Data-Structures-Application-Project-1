namespace Project_1.Model;

public class TaskItem : IComparable<TaskItem>
{
    public int Id { get; init; }
    public required string Description { get; init; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public string? AssignedTo { get; set; }

    public int CompareTo(TaskItem? other)
    {
        if (other == null) return 1;
        return this.Id.CompareTo(other.Id);
    }

    public override string ToString()
    {
        return $"Id: {Id} | {Description} | Priority: {Priority} | Status: {Status}";
    }
}