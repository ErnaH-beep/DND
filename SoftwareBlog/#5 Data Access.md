# Data Access Layer Implementation

## Overview

Our project implements a robust data access layer using Entity Framework Core 9.0 with SQLite as the database provider. This implementation follows the Repository pattern through services, providing a clean separation of concerns between the data access logic and business logic.

### Why Entity Framework Core?

We chose Entity Framework Core as our Object-Relational Mapper (ORM) for its comprehensive feature set and modern architecture. It provides strong type safety with compile-time checking and IntelliSense support, making development more efficient and reducing runtime errors. The integration with LINQ enables powerful and intuitive querying capabilities, while its cross-platform nature ensures our application runs seamlessly across different environments.

- Database Context
  - The core of our data access layer is implemented through the `AppDbContext` class, which inherits from `DbContext`. This class serves as the primary point of interaction with our database.
- Entity Models
  - Our entities are defined in the Shared project, making them accessible across different layers of the application. The model structure implements interfaces for consistency and includes Data Transfer Objects (DTOs) for efficient data transformation.
- CRUD Operations
  - ORM provides a clean and efficient way to perform CRUD operations on our database.
  - This reduces the amount of boilerplate code and allows us to focus on the logic of our application.

### LINQ vs SQL

In traditional SQL, we write raw SQL queries to interact with our database. This approach is powerful but can be error-prone and difficult to maintain.
An example of a SQL query is:

```sql
SELECT * FROM People WHERE Role = 'Manager';
```

With LINQ, we can write more readable and maintainable code. An example of a LINQ query is:

```csharp
var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
```

- Advantages of LINQ:
  - More intuitive and type-safe way to query our database.
  - Easier to read and maintain.
  - Integrated with Entity Framework Core, so we can easily translate our queries to database operations.

Code example of a LINQ in EFC:

```csharp
public async Task<string> UpdateTaskStatus(int taskId, string newStatus)
{
    var task = await _dbContext.Tasks.FindAsync(taskId) ?? throw new KeyNotFoundException("Task not found");
    task.Status = newStatus;
    task.ModifiedOn = DateTime.Now;
    await _dbContext.SaveChangesAsync();
    return "Task status updated successfully";
}
```

### Why SQLite?

SQLite serves as our database provider due to its simplicity and efficiency. Unlike traditional database systems, SQLite doesn't require a separate server process, as the entire database is stored in a single file. This makes deployment and maintenance significantly easier. Its ACID compliance ensures data reliability with excellent crash recovery capabilities, while its performance characteristics make it ideal for small to medium-sized applications like ours.

## Database Context

The core of our data access layer is implemented through the `AppDbContext` class, which inherits from `DbContext`. This class serves as the primary point of interaction with our database:

```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Task> Tasks { get; set; }
    public DbSet<ProjectModel> Projects { get; set; }
    public DbSet<PersonBase> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonBase>()
            .HasDiscriminator<string>("Role")
            .HasValue<Employee>("Employee")
            .HasValue<Manager>("Manager");

        // Entity configurations
        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Title).IsRequired();
            entity.Property(t => t.Status).IsRequired();
        });

        modelBuilder.Entity<ProjectModel>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired();
            entity.Property(p => p.ManagerId).IsRequired();
        });
    }
}
```

## Service Layer Implementation
Our data access is orchestrated through services that act as repositories, with each service handling data operations for a specific entity type. Here's an example of our PersonService implementation:

```csharp
public class PersonService
{
    private readonly AppDbContext _dbContext;

    public PersonService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PersonBase>> GetPeople(string? role = null, bool? active = null)
    {
        var query = _dbContext.People.AsQueryable();

        if (!string.IsNullOrEmpty(role))
        {
            query = query.Where(p => p.Role == role);
        }

        if (active.HasValue)
        {
            query = query.Where(p => p.IsActive == active.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<PersonBase?> GetPersonById(string employeeId)
    {
        return await _dbContext.People.FirstOrDefaultAsync(p => p.EmployeeId == employeeId);
    }
}
```

### Service Layer Design Patterns

Our service layer implements several key design patterns to ensure efficient and maintainable code. The Repository Pattern is implemented through services, where each service acts as a specialized repository for its entity type. This encapsulation of data access logic provides a clean API for the business layer and facilitates unit testing through dependency injection.

Query building in our services utilizes deferred execution through `IQueryable`, allowing us to build queries dynamically based on parameters. This approach optimizes database queries by applying filters before execution, reducing unnecessary data retrieval.

We've implemented the async/await pattern throughout our data access layer to improve application scalability. This asynchronous approach prevents thread blocking and ensures better resource utilization, particularly important when handling multiple concurrent requests.

## Entity Models

Our entities are defined in the Shared project, making them accessible across different layers of the application. The model structure implements interfaces for consistency and includes Data Transfer Objects (DTOs) for efficient data transformation. Here's our Person model structure:

```csharp
public interface IPerson : IPersonDTO
{
    string EmployeeId { get; set; }
}

public class PersonBase : IPerson
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}

public class Employee : PersonBase { }
public class Manager : PersonBase { }
```

### Entity Design Patterns

Our entity design implements interface segregation to separate different concerns. The `IPersonDTO` interface handles data transfer operations, while `IPerson` manages entity-specific operations. This separation allows for more flexible and maintainable code.

The inheritance hierarchy in our entity design places common properties in the base class, with derived classes for specific roles. This approach enables polymorphic behavior while maintaining code reusability. We've also implemented audit fields (`CreatedOn`, `ModifiedOn`) and soft delete functionality (`IsActive`) to track entity lifecycle and maintain data history.

## Best Practices and Performance Considerations

Our data access implementation adheres to several key best practices. We maintain a strict separation of concerns, with data access logic isolated in services and business logic in a separate layer, following clean architecture principles.

The codebase is organized with a logical folder structure and consistent naming conventions, making it easy to navigate and maintain. Error handling is implemented comprehensively, with proper exception handling and meaningful error messages to facilitate debugging and provide better user feedback.

Performance optimization is achieved through several mechanisms:

- Efficient query building using LINQ and IQueryable
- Proper database indexing on frequently queried fields
- Strategic use of lazy loading to prevent unnecessary data retrieval
- Support for batch operations to reduce database round trips

These practices ensure our data access layer remains performant, maintainable, and secure while providing a solid foundation for the application's data management needs.
