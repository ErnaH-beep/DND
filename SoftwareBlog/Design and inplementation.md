# Design and Structure of the Codebase
The project is a web application built using Blazor, a popular framework for building interactive web applications. It is designed to be a project and task management system that allows users to create, assign, and track tasks within projects. The application is built using a client-server architecture, with a focus on using RESTful APIs to communicate between the client and server.

## Overview
The codebase is structured to support a web application that manages tasks and projects. It is built using Blazor for the frontend and ASP.NET Core for the backend. The application is designed to be modular, with a clear separation of concerns between different components and services.

## Technologies Used
- **Blazor**: A framework for building interactive web applications using C# and .NET.
- **ASP.NET Core**: A framework for building web apps and services.
- **Entity Framework Core**: An object-relational mapper (ORM) for .NET to work with databases.
- **SQLite Server**: A relational database management system.

## Frontend Structure
The frontend is implemented using Blazor, which allows for building interactive web UIs using C#. The main components of the frontend include:
- **Pages**: These are the main views of the application, such as the dashboard and task board.
- **Modals**: Used for actions like editing or deleting tasks.

#### Example: Landing Page
The `LandingPage.razor` file defines the structure of the landing page, including a header and a hero section.

#### Styling
Styling is managed using CSS and a bit of Bootstrap, with a focus on modern design principles. The `LandingPage.razor.css` file provides styles for the landing page.

## Backend Structure
The backend is built using ASP.NET Core, providing RESTful APIs for the frontend to interact with. The backend is organized into controllers and services.

#### Controllers
Controllers handle HTTP requests and are responsible for the business logic of the application. They interact with services to perform operations.

#### Endpoints
The endpoints are defined in the `TasksController`, `ProjectsController`, and `PeopleController` files.

##### `TasksController`
| HTTP Method | Endpoint                | Description           |
|-------------|-------------------------|-----------------------|
| GET         | /api/tasks              | Get all tasks         |   
| GET         | /api/tasks/{id}          | Get task by id        |
| POST        | /api/tasks              | Create a new task     |
| PUT         | /api/tasks/{id}          | Update a task         |
| PUT         | /api/tasks/{id}/status  | Update task status     |
| DELETE      | /api/tasks/{id}          | Delete a task         |

##### `ProjectsController`
| HTTP Method | Endpoint                | Description           |
|-------------|-------------------------|-----------------------|
| GET         | /api/projects              | Get all projects   |   
| POST        | /api/projects              | Create a new project  |

##### `PeopleController`
| HTTP Method | Endpoint                  | Description                           |
|-------------|---------------------------|---------------------------------------|
| GET         | /api/people               | Get all people, optionally filtered by role and active status |
| GET         | /api/people/{id}  | Get a person by employee ID           |
| POST        | /api/people/register      | Register a new person                 |
| POST        | /api/people/login         | Login a person and generate a JWT     |
| PUT         | /api/people/{id}  | Update a person's details             |
| DELETE      | /api/people/{id}  | Deactivate a person                   |


#### Example: Tasks Controller
The `TasksController` manages task-related operations, such as retrieving and updating tasks.

    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
    }

#### Services   
Services encapsulate the business logic and data access. They interact with the database and provide data to the controllers.

#### Example: Task Service
The `TaskService` provides methods to interact with task data.

    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        public async Task<Task?> GetTaskById(int id)
        {
            return await _dbContext.Tasks.FindAsync(id);
        }
    }

## Conclusion
The codebase is designed to be modular and maintainable, with a clear separation between the frontend and backend. The use of Blazor and ASP.NET Core allows for a modern, responsive web application that is easy to extend and maintain.