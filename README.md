# OutOfOffice

Is an ASP.NET MVC application designed to manage absence notifications. 

## How To Use

  1. Clone the repository.
  2. Make sure that MySQL is installed on your PC.
  3. Change the database connection string in the appsettings.json file to your own.
  4. Apply migrations with the ```Update-Database``` or ```dotnet ef database update``` command.

## Main Pages

  - /Home - page for displaying the user's name and role.
  - /Lists/Employees - page for displaying all employees.
  - /Lists/Projects - page for displaying all projects.
  - /Lists/LeaveRequests - page for displaying all leave requests.
  - /Lists/ApprovalRequests - page for displaying all approval requests.
  - /Home/Login - login page.
    
## Pages For Changing Information

  - /Lists/AddEmployee - page for adding an employee to the database.
  - /Lists/EditEmployee/{int} - page for changing data about an existing employee.
  - /Lists/AddProject -  page for adding an project to the database.
  - /Lists/AddEditLeaveRequest -  page for adding a leave request to the database.
  - /Lists/AddEditLeaveRequest/{int} - page for changing data about an existing leave reques.
    
 ## Roles
 Unauthorized users cannot access other pages of the site except the home page.

The system has 3 user roles:
- **HR Manager**: Can view main pages, perform CRUD operations with employees, approve and reject requests.
- **Project Manager**: Can view main pages, approve and reject requests, create and delete projects.
- **Employee**: Can view /Lists/LeaveRequests with their requests and /Lists/Projects.
     
## Test Data

The database is empty, so you have to enter data manually

## License

This project is licensed under the MIT License - see the LICENSE.md file for details
