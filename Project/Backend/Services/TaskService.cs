using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Project.Backend.Services
{
    public class TaskService
    {
        private readonly AppDbContext _dbContext;

        public TaskService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Task>> GetTasksByProject(string projectId)
        {
            return await _dbContext.Tasks
                .Where(t => t.ProjectId == projectId && t.IsActive)
                .ToListAsync();
        }

        public async Task<List<Task>> GetTasksByAssignee(string employeeId)
        {
            return await _dbContext.Tasks
                .Where(t => t.AssignedToId == employeeId && t.IsActive)
                .ToListAsync();
        }

        public async Task<string> CreateTask(Task task)
        {
            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();
            return "Task created successfully";
        }

        public async Task<string> UpdateTaskStatus(int taskId, string newStatus)
        {
            var task = await _dbContext.Tasks.FindAsync(taskId);
            if (task == null) throw new KeyNotFoundException("Task not found");
            task.Status = newStatus;
            task.ModifiedOn = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return "Task status updated successfully";
        }

        public async Task<List<Task>> GetAllTasks()
        {
            return await _dbContext.Tasks
                .Where(t => t.IsActive)
                .ToListAsync();
        }
    }
} 