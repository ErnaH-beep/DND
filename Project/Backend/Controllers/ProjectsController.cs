using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Backend.Services;

namespace Project.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(ProjectService projectService, ILogger<ProjectsController> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectModel>>> GetProjects()
        {
            try
            {
                var projects = await _projectService.GetAllProjects();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving projects");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectModel project)
        {
            try
            {
                await _projectService.CreateProject(project);
                return Ok("Project created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating project");
                return BadRequest(ex.Message);
            }
        }
    }
} 