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

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<List<Task>>> GetProjectTasks(string projectId)
        {
            try
            {
                var tasks = await _taskService.GetTasksByProject(projectId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving project tasks");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Task task)
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

        [HttpGet]
        public async Task<ActionResult<List<Task>>> GetTasks()
        {
            try
            {
                var tasks = await _taskService.GetAllTasks();
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