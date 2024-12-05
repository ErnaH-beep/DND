# User Management

## Users in the system
The system is designed to manage two types of users:
- **Admin(Manager)**: These users have full access to all system functionalities, including managing other users and viewing sensitive data.
- **Regular User(Employee)**: These users have limited access, primarily interacting with the system based on assigned permissions (e.g., viewing their own data or using basic functionalities).

### Implementation of Log-In Functionality:
The login functionality is implemented in the `Login` endpoint within the `PeopleController`. It uses user credentials stored in the database to authenticate users and determine their access level.

### Steps in the Login Process:
1. User Input: The user provides their employee ID and password via the login form.
2. Database Query: The system queries the database using `_personService.GetPersonById()` to check if the entered employee ID exists.
3. Password Verification: The entered password is verified against the hashed password using BCrypt.
4. JWT Creation: Upon successful login, a JWT token is generated containing claims about the user.
5. Access Levels: Based on the user's role, appropriate access rights are granted through the JWT token.

### User Input
  The login endpoint accepts a `PersonBase` object containing the user's credentials through the request body:
```csharp
[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] PersonBase person)
```


### Database Query:
The system checks if the user exists and is active:

```csharp
var foundPerson = await _personService.GetPersonById(person.EmployeeId);

if (foundPerson == null || !foundPerson.IsActive)
{
    return Unauthorized("Invalid employee ID or inactive account.");
}
```

This ensures that only registered active users can attempt to log in.

### Password Verification:
BCrypt is used to verify the password against the stored hash:
```csharp
bool isPasswordValid = BCrypt.Net.BCrypt.Verify(person.Password, foundPerson.Password);
if (!isPasswordValid)
{
    return Unauthorized("Invalid password.");
}
```
Why use password hashing?
Storing hashed passwords improves security by protecting against data breaches. Even if the database is compromised, attackers cannot retrieve plain-text passwords.

### Authentication and Session Management:
The system uses JWT (JSON Web Tokens) for authentication. Upon successful login, a JWT token is generated:
```csharp
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

var token = new JwtSecurityToken(
    _config["Jwt:Issuer"],
    _config["Jwt:Issuer"],
    null,
    expires: DateTime.Now.AddMinutes(120),
    signingCredentials: credentials
);
```
Why use JWT?
JWTs provide a secure, stateless way to handle user authentication and authorization. They contain all necessary user information and are cryptographically signed to make sure of the integrity.

### Role based access:
The system includes the user's role in the JWT response:
```csharp
var response = new { Token = jwtToken, EmployeeId = foundPerson.EmployeeId, Role = foundPerson.Role };
```
This ensures that users only access pages and features relevant to their roles.

### Error Handling:
The system provides appropriate error responses for various scenarios:
- Invalid employee ID or inactive account
- Invalid password
- Other potential errors during the authentication process

These responses help users understand why their login attempt failed and what actions they need to take.

## Access to resources
### Role-Based Access Control
The system implements role-based access control (RBAC) through JWT tokens and user roles. When a user logs in, their role is included in the JWT response:
```csharp
var response = new { Token = jwtToken, EmployeeId = foundPerson.EmployeeId, Role = foundPerson.Role };
```
This role information is then stored in local storage for frontend access control:
```csharp
await LocalStorage.SetItemAsync("authToken", token);
await LocalStorage.SetItemAsync("employeeId", employeeId);
await LocalStorage.SetItemAsync("userRole", role);
```

### Resource Access Patterns
**Manager Access:**
- Full access to user management functionality
- Can view and modify all users' information
- Access to the management dashboard
- Can create tasks for any user

**Employee Access:**
- Limited to viewing and modifying their own information
- Can only create and manage their own tasks
- No access to user management features

### Secure Sensitive Data 
Passwords are hashed using BCrypt, and the service verifies the hash during login. 

```csharp
bool isPasswordValid = BCrypt.Net.BCrypt.Verify(person.Password, foundPerson.Password);
if (!isPasswordValid)
{
    return Unauthorized("Invalid password.");
}
```

