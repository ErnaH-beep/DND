# TASKER - Task Manager :calendar:

### Problem:  

Individuals need a simple way to manage and track their tasks. 

 
### Solution:  

- A website where users can create, update, and delete tasks.  

- Different user roles such as Admin (who can manage all tasks) and Regular Users (who can manage their own tasks). 

 
### Features: 

- Users can log in and then view or edit their task list/s. 

- RESTful API for CRUD-operations on tasks. 

- Store user data using an Object Relational Mapper and SQLite to store tasks and user data. 

 
### Why did we choose to create a task manager: 

We decided to develop a task manager because it addresses a very common and relatable problem: the need to organize and manage tasks efficiently. A task management system is both a very practical and decently simple project, making it a good choice as this is our first experience developing a website using .NET development. 

The task manager should allow users to create, update, delete, and track tasks in an intuitive and user-friendly way. It also meets the technical requirements for handling multiple actors by including roles such as Admin and Regular Users, making it good for practicing user authentication and authorization. Furthermore, the choice of creating a task manager lets us implement the technical requirements, including the RESTful API, Blazor UI, and integration with SQLite for data storage. 



## User Stories

### User Role:  Regular 

#### User Registration: 

As a user, I want to be able to register with my email and password so that I can create an account and start managing tasks.  

#### User Login: 

As a user, I want to log in with my email and password so that I can access my task list securely. 

#### Create Task: 

As a user, I want to create new tasks by providing a title and description so that I can track things I need to do. 

#### View Tasks: 

As a user, I want to view a list of all my tasks so that I can easily see what I need to work on. 

#### Edit Task: 

As a user, I want to edit the details (title, description, due date) of a task so that I can update the task information as needed. 

#### Delete Task: 

As a user, I want to delete a task so that I can remove tasks that are no longer relevant. 

#### Mark Task as Complete: 

As a user, I want to mark a task as complete so that I can keep track of which tasks I have finished. 

#### Filter or Sort Tasks: 

As a user, I want to filter or sort tasks by status (completed, not completed) and due date so that I can prioritize my work more effectively. 

#### Logout: 

As a user, I want to be able to log out of the system so that my account is secure. 

 
### User Role: Admin 

#### View All Users’ Tasks: 

As an admin, I want to view all users' tasks so that I can monitor overall task progress and identify issues. 

#### Edit Any User’s Task: 

As an admin, I want to edit any user’s task so that I can help manage and maintain tasks for other users. 

#### Delete Any User’s Task: 

As an admin, I want to delete any user’s task to remove irrelevant or inappropriate tasks from the system. 

#### Create Task for Any User: 

As an admin, I want to create tasks for any user to help assign tasks and manage workload. 

#### Manage User Roles: 

As an admin, I want to assign and manage roles (Admin, Regular User) so that I can control permissions within the system. 

 
