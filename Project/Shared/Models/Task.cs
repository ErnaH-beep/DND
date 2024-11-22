namespace Shared.Models;
public class Task
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = "Todo"; // Todo, InProgress, Done
    public string Priority { get; set; } = "Medium"; // Low, Medium, High
    public string? AssignedToId { get; set; } // EmployeeId of assignee
    public string? ProjectId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime? ModifiedOn { get; set; } = DateTime.Now;
} 