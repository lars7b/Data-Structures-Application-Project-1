namespace Project_1.Model;

public class TaskItem
{
    public int Id { get; init; }
    public required string Description { get; init; }
    public bool Completed { get; set; }
    public string? AssignedTo{get;set;}

    public override string ToString()
    {
        var status = Completed ? "[✓]" : "[ ]";
        return AssignedTo == null ? $"{status} {Id}: {Description}": $"{status} Assigned To: {AssignedTo}: {Id}: {Description}";
    }
}