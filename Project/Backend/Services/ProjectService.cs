using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Project.Backend.Services
{
    public class ProjectService
    {
        private readonly AppDbContext _dbContext;

        public ProjectService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProjectModel>> GetAllProjects()
        {
            return await _dbContext.Projects
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<string> CreateProject(ProjectModel project)
        {
            project.CreatedOn = DateTime.Now;
            project.IsActive = true;
            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync();
            return "Project created successfully";
        }
    }
} 