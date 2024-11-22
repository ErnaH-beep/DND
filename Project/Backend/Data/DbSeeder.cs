using Backend.Data;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static async System.Threading.Tasks.Task SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Check if we already have any users
        if (!context.People.Any())
        {
            // Create test manager
            var testManager = new Manager
            {
                Name = "Test Manager",
                Email = "test@manager.com",
                EmployeeId = "123456",
                Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                Role = "Manager",
                IsActive = true
            };

            context.People.Add(testManager);
            await context.SaveChangesAsync();
        }
    }
} 