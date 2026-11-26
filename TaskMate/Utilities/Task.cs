namespace TaskMate.Utilities;

internal sealed class TaskItem
{
    public int TaskId { get; set; }
    public int UserId { get; set; } = 1;
    public int? CategoryId { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Priority { get; set; } = "Medium";
    public string Status { get; set; } = "Pending";
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
}
