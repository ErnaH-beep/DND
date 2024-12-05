# Final Development Report

## Table of Contents
1. [Final Development Update](#final-development-update)
2. [Project Outcome](#project-outcome)
3. [Updated Requirements List](#status-requirements-list)
4. [Short Demonstration Video](#demonstration-video)
5. [Delimitation](#delimitation)
6. [Final Project Conclusion](#final-project-conclusion)



---

## Final Development Update

1. **Backend Infrastructure**
   - Implemented robust task management system with CRUD operations
   - Established secure user authentication and authorization
   - Developed role-based access control system for different user types
   - Created API endpoints for task operations and user management

2. **Frontend Development**
   - Designed and implemented an intuitive user interface
   - Created interactive dashboard with visual data representations
   - Developed a functional Kanban board for task visualization
   - Implemented calendar view for better task planning
   - Added filtering and sorting capabilities for task management

3. **User Management**
   - Implemented user authentication system with login/logout functionality
   - Developed role-based permissions (Employee and Manager roles)
   - Created user role management system for Managers
   - Set up access control mechanisms for task visibility and editing

4. **Task Management**
   - Implemented comprehensive task CRUD operations
   - Added task status tracking functionality
   - Created task filtering and sorting capabilities
   - Developed task assignment and ownership features
   - Implemented task completion marking system

5. **Data Access**
   - Established secure data access patterns based on user roles
   - Implemented data visibility controls
   - Created efficient data retrieval methods for tasks and user information
   - Set up proper data management for task updates and modifications

6. **Final Testing and Optimization**
   - Conducted thorough testing of all implemented features
   - Optimized application performance
   - Fixed identified bugs and issues
   - Ensured smooth user experience across all functionalities

## Project Outcome
The application fullfills its objectives. The application is a prototype and further development is required for it to sucessfully act as a task management tool to assist teams and businesses. Key features that are currently implemented:
- **Time & Task Management**: The application offers task planning to every user, which greatly assists in keeping an overview of the project.
- **User Roles**: The use of user roles asisst the team or company to assign tasks and effecciently control the project and tasks within. This is made access control and data management techniques.
- **Visuals**: Visuals, as on the dashboard and the kanban board, makes the application more intuitive for the users. Furthermore it assist in getting a quick data based overview of the project status.


## Status Requirements List


| Requirement                                               | Status        |
|-----------------------------------------------------------|---------------|
| User can register with their email and create a password| ❌ Not Implemented|
| Users can create and view tasks                         | ✅ Implemented |
| Users can edit and delete tasks                         | ✅ Implemented |
| Users can mark tasks as complete                        | ✅ Implemented |
| Users can filter and sort tasks                         | ✅ Implemented |
| Users can login and logout of their respective accounts | ✅ Implemented |
| Users roles will be applied to each account             | ✅ Implemented |
| User role "Employee" will only be able to edit and interact with their own tasks, while they can view other users tasks. | ✅ Implemented |
| User role "Manager" can edit and interract with all tasks and can change the role of other users. | ✅ Implemented |

## Demonstration to Video
[Watch the demo of the application here](https://www.youtube.com/watch?v=6_F6r3z9-Dg)

## Delimitation
Throughout the project, several potential improvements and limitations were identified:

- **User Registration**: While the login system is functional, the self-registration feature wasn't implemented. Currently, user accounts need to be created manually by system administrators. This could be enhanced in future iterations with a complete registration flow.

- **Task ID System**: The current task identification system could be improved to include more detailed categorization and better sorting capabilities. A more comprehensive ID system could help in better tracking and organizing tasks across different projects or departments.

- **Calendar Functionality**: The calendar view is a basic implementation and could be enhanced to include more features.

- **Project Functionality**: The project functionality is limited to the creation of a project and the assignment of tasks to the project. The project itself has no specific function, but is only used to group tasks together.

- **Advanced Analytics**: The dashboard provides basic visual representations, but there's potential for more advanced analytics features such as:
  - Detailed performance metrics
  - Time tracking analytics
  - Project progress predictions
  - Resource allocation analysis

- **Mobile Responsiveness**: While the application works on desktop browsers, a fully responsive mobile version could enhance accessibility and user convenience.

These limitations were identified but left out of the current scope due to time constraints and the prototype nature of the application. They represent opportunities for future development and enhancement of the system.

## Final Project Conclusion
All in all, the project turned out successfully. The initial requirements were met, with the exception of the registration functionality for the users. Additionally, a calendar, a kanban board, and visuals on the dashboard have been implemented to enhance planning, tracking, and overall user experience.
The implementation of user-roles adds significant value to teams and organizations by ensuring clear responsibilities and efficient task management. Employees can manage their own tasks while viewing all, and managers can oversee projects and adjust roles as needed.

