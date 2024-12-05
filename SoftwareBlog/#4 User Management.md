# User Management

## Users in the system
The system is designed to manage two types of users:
* Admin(Manager): These users have full access to all system functionalities, including managing other users and viewing sensitive data.
* Regular User(Employee) : These users have limited access, primarily interacting with the system based on assigned permissions (e.g., viewing their own data or using basic functionalities).

### Implementation of Log-In Functionality:
The login functionality is implemented in the `login_user()` function. It uses user credentials stored in the database to authenticate users and determine their access level.

### Steps in the Login Process:
User Input: The user provides their username and password via the login form.
Database Query: The system queries the database to check if the entered username exists.
Password Verification: The hashed password stored in the database is compared with the entered password using a hashing function.
Session Creation: Upon successful login, the user's ID is stored in the session to track their authentication status.
Access Levels: Based on the user type (e.g., manager or employee), the system redirects them to the appropriate dashboard or page.

The login process is securely implemented to authenticate users and grant appropriate access based on their roles.
The route `/login` is defined to handle both GET and POST requests:
* GET Request: Loads the login form for the user to enter their credentials.
* POST Request: Processes the submitted form data to authenticate the user.

### User Input
  The `request.form.get()` method retrieves the `username` and `password` entered by the user in the login form.
```
username = request.form.get('username')
password = request.form.get('password')
```
Why `request.form.get()?`
It safely retrieves data from the form fields and avoids errors if a field is missing.

### Database Query:
The `User.query.filter_by(username=username).first()` method searches the database for a user with the matching username. If no match is found, the user is notified that the username doesn’t exist.

```user = User.query.filter_by(username=username).first()```

This ensures that only registered users can attempt to log in.

### Password Verification:
`The check_password_hash()` function verifies the password entered by the user against the hashed password stored in the database.
```
if check_password_hash(user.password, password):
```
Why use password hashing?
Storing hashed passwords improves security by protecting against data breaches. Even if the database is compromised, attackers cannot retrieve plain-text passwords.

### Authentication and Session Management:
The `login_user()` function from the Flask-Login library is used to log the user into the application. It stores the user’s authentication state in the session.
```
login_user(user)
```
Why use Flask-Login?
It simplifies user authentication by managing sessions, login status, and user-specific data securely.

### Role based redirection:

After successful login, the user is redirected to their designated dashboard:

Manager: Redirected to `admin_dashboard`.
Employee: Redirected to `user_dashboard`.
```
return redirect(url_for('admin_dashboard') if user.is_admin else url_for('user_dashboard'))
```

This ensures that users only access pages and features relevant to their roles.

### Error Handling:

If the username or password is invalid, the user is shown appropriate error messages using Flask’s `flash()` function.
```
flash('Invalid password. Please try again.', 'error')
flash('Username does not exist. Please register first.', 'error')
```

Why use flash messages?
They provide real-time feedback to users, improving the user experience.



