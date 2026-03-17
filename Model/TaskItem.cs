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
        var status = Status switch
        {
            Status.Todo => "[To Do]",
            Status.InProgress => "[In Progress]",
            Status.Done => "[Done]",
            _ => "[To Do]"
        };

        return AssignedTo == null
            ? $"Id: {Id}, Status: {status}, Description: {Description}, Priority: {Priority}, Date: {CreatedAt.ToShortDateString()}"
            : $"Assigned To: {AssignedTo}, Id: {Id}, Status: {status}, Description: {Description}, Priority: {Priority}, Date: {CreatedAt.ToShortDateString()}";
    }
}