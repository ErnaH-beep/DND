using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Backend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly ILogger<TasksController> _logger;

        public TasksController(TaskService taskService, ILogger<TasksController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Shared.Models.Task task)
        {
            try
            {
                await _taskService.CreateTask(task);
                return Ok("Task created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{taskId}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int taskId, [FromBody] string status)
        {
            await _taskService.UpdateTaskStatus(taskId, status);
            return Ok("Task status updated successfully");
        }

        [HttpGet]
        public async Task<ActionResult<List<Shared.Models.Task>>> GetTasks()
        {
            try
            {
                var tasks = await _taskService.GetAllTasks();
                Console.WriteLine("tasks from all tasks: " + tasks);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tasks");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
} 