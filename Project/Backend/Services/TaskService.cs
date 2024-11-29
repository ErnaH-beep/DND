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

        public async Task<List<Shared.Models.Task>> GetTasksByProject(string projectId)
        {
            return await _dbContext.Tasks
                .Where(t => t.ProjectId == projectId && t.IsActive)
                .ToListAsync();
        }

        public async Task<List<Shared.Models.Task>> GetTasksByAssignee(string employeeId)
        {
            return await _dbContext.Tasks
                .Where(t => t.AssignedToId == employeeId && t.IsActive)
                .ToListAsync();
        }

        public async Task<string> CreateTask(Shared.Models.Task task)
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

        public async Task<List<Shared.Models.Task>> GetAllTasks()
        {
            return await _dbContext.Tasks
                .Where(t => t.IsActive)
                .ToListAsync();
        }

        public async Task<string> UpdateTask(int taskId, Shared.Models.Task updatedTask)
        {
            try
            {
                var existingTask = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);

                if (existingTask == null)
                {
                    return "Task not found";
                }

                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.DueDate = updatedTask.DueDate;
                existingTask.Priority = updatedTask.Priority;
                existingTask.ProjectId = updatedTask.ProjectId;
                existingTask.AssignedToId = updatedTask.AssignedToId;
                existingTask.ModifiedOn = DateTime.Now;

                await _dbContext.SaveChangesAsync();
                return "Success!";
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error in updateTask: {ex.Message}");
                return ex.Message;
            }
            
        }
    }
}