namespace TaskMate.Utilities;

internal sealed class ScheduleItem
{
    public int ScheduleId { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
