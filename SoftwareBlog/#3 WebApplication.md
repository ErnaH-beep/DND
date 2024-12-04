# Web Application

## How are our key requirements implemented in our web application? 

### User Roles 

Admin (Manager): 

* Can create tasks for any user via the `CreateTask` Method in `TasksController`. 

* Has permission to edit or delete tasks belonging to any user 

* Can view tasks for all users using `GetTasks` 

Regular Users (Employees): 

* Can only create, view, edit, or delete their own tasks.  

* Cannot manage tasks belonging to other users. 

The implementation of role-based access is achieved via conditional logic within the service layer, ensuring that access is restricted based on user roles. 

 

### Tasks Management:  

The `TasksController` manages all task-related operations: 

Regular Users: 

* Can create tasks for themselves using  `CreateTask`. 

* Edit or update their tasks using  `UpdateTask`. 

* Mark tasks as complete through  `UpdateTaskStatus`. 

* Retrieve their tasks with  `GetTasks`. 

Admin: 

* Can create tasks for others. 

* Access and manage all tasks using `GetTasks`, `UpdateTask`, or `DeleteTask`. 

### RESTful API 

* CRUD operations are implemented using a RESTful approach, following HTTP verb conventions (POST for creating, GET for reading, PUT for updating, and DELETE for deleting). 


### Database Integration 

* The application stores user and task data using an ORM (e.g., Entity Framework) and SQLite.


### Users Can Create, Update, and Delete Tasks

#### Create Task

This is handled by the TaskService and various frontend components:

Code Example: Creating a Task (TaskService)

 ``` public async Task<string> CreateTask(Shared.Models.Task task)
{
    _dbContext.Tasks.Add(task);
    await _dbContext.SaveChangesAsync();
    return "Task created successfully";
}
```

The CreateTask method adds a new task to the database. It ensures the task is associated with the correct project or user via properties like AssignedToId.


#### Updating Tasks

``` public async Task<string> UpdateTask(int taskId, Shared.Models.Task updatedTask)
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
    existingTask.ModifiedOn = DateTime.Now;

    await _dbContext.SaveChangesAsync();
    return "Success";
}
```

The `UpdateTask` method ensures tasks can be edited efficiently while preserving database consistency.

#### Deleting Tasks
```
public async Task<string> DeleteTask(int taskId)
{
    var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);

    if (task != null)
    {
        task.IsActive = false; // Soft delete
        task.ModifiedOn = DateTime.Now;
        await _dbContext.SaveChangesAsync();
        return "Success";
    }
    return "Task ID not found.";
}
```

This method implements a soft delete, allowing data retention while marking tasks as inactive.

## Pages

#### Login Page:
Provides a login form for users to authenticate and access the system.
Key Features:
* Login Form: Users must submit a form with their credentials (username/password).
* Form Validation: Ensures the form is correctly filled before submission.
* Navigation: On successful login, the user is redirected to the appropriate page.

Components:
Displays a logo.
An `EditForm` for entering login credentials.
User Authentication: Handles the login process and stores the authentication token in local storage.

#### Calendar: 
* Displays a calendar view of tasks using the Radzen Scheduler component.
* Allows users to view tasks on a calendar with support for year, month, and day views.
* Tasks are styled based on priority: high (red), medium (orange), and low (green).
* Fetches task data from the backend API (`api/Tasks/`) and dynamically updates the calendar.
`Calendar.css` manages the layout and appearance of the calendar container.

#### Home Page: 
Serves as the task dashboard, providing visual insights into task data.
Features:
* Line Chart: Displays tasks created over the last 7 days.
* Pie Chart: Shows the distribution of tasks by status.
* Bar Chart: Illustrates the distribution of tasks by priority.
Charts are dynamically generated using data retrieved from the backend API (`api/Tasks/`).


#### Manage Users Page
Purpose: This page is used for managing user accounts. It allows admins to view, add, edit, or delete users.
Key Features:
* User Listing: Displays users in a table with columns such as Employee ID, Name, Email, Role, and timestamps for creation and modification.
* Actions: Users can be edited or deleted from a dropdown menu.
* Add User: A button to show the RegisterUserModal for adding a new user.
Modals:
* RegisterUserModal for registering a new user.
* EditUserModal for editing user details.
* DeleteUserModal for deleting users.
* Loading State: Displays a loading message while users are being fetched.

#### Tasks Page: 
This page displays tasks for a specific project, or "My Tasks" for the logged-in user. It includes a table with task details like title, description, project, assigned user, due date, status, and priority.

#### Kanban board:
This page represents a Kanban board where tasks can be organized into columns representing different stages of work (e.g., "Todo", "InProgress", "Done"). Users can drag tasks from one column to another, indicating progress on the task. The page interacts with the backend API to load and update tasks and related data.

Drag-and-Drop Functionality
* Each task card is draggable if the user has the appropriate permissions. This is controlled by the `canDrag` variable, which is set based on the user's role or if the task is assigned to the user.
* When a task card is dragged, the `OnDragStart` method is called to store the task being dragged (`draggedTask`).
* When a task is dropped into a new column (`OnDrop`), the task's status is updated, and the change is sent to the backend.

## How the frontend connects to the web service

Setting Up the HTTP Client:
In the Program.cs file, HttpClient is set up that the frontend uses to communicate with the backend. Specifically, we created a named client called "BackendAPI":

```
builder.Services.AddHttpClient("BackendAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001");
});
```

Frontend Request Handling: In our Blazor components, fx. the Kanban board page, we can now make HTTP requests using the client we set up. For example, to fetch data (such as tasks, projects, or people), we use HttpClient to call the API and retrieve the necessary data.

Here's an example from the OnInitializedAsync method of the Kanban board page where we fetch tasks and projects:
```
var client = ClientFactory.CreateClient("BackendAPI");
Tasks = await client.GetFromJsonAsync<List<Shared.Models.Task>>("api/Tasks");
Projects = await client.GetFromJsonAsync<List<ProjectModel>>("api/Projects");
```
This sends a GET request to the backend API endpoints api/Tasks and api/Projects and retrieves the data as JSON, which is then used to populate the frontend UI.

Sending Data to the Backend: Then when needed to update data, such as changing the status of a task, a PUT request is sent to the backend to update the task status. For example, in the Kanban board, when a task is dragged and dropped into a new column, the new status is sent to the backend with a request like this: 
```
await client.PutAsJsonAsync($"api/Tasks/{draggedTask.Id}/status", newStatus);
```

This sends a PUT request to update the task's status on the backend, ensuring that the change is persisted.
