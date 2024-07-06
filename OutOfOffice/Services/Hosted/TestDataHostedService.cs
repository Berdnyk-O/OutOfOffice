using OutOfOffice.Enums;
using OutOfOffice.Helpers;
using OutOfOffice.Managers;
using OutOfOffice.Models;
using OutOfOffice.Models.Entities;

namespace OutOfOffice.Services.Hosted
{
    public class TestDataHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public TestDataHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var manager = scope.ServiceProvider.GetRequiredService<IManager>();
            
            manager.ClearData();
            
            var employees = new AddEditEmployeeViewModel[]
            {
                new()
                {
                    FullName = "Лучук Орина Северинівна",
                    Subdivision = Subdivision.HumanResources,
                    Position = Position.HRManager,
                    Status = Status.Active,
                    OutOfOfficeBalance = 100
                },
                new()
                {
                    FullName = "Федорчук Мирослав Остапович",
                    Subdivision = Subdivision.Development,
                    Position = Position.ProjectManager,
                    Status = Status.Active,
                    OutOfOfficeBalance = 124
                },
                new()
                {
                    FullName = "Янівська Леля Володимирівна",
                    Subdivision = Subdivision.Development,
                    Position = Position.Developer,
                    Status = Status.Active,
                    OutOfOfficeBalance = 100
                },
                new()
                {
                    FullName = "Щербина Пребислав Панасович",
                    Subdivision = Subdivision.Development,
                    Position = Position.Developer,
                    Status = Status.Active,
                    OutOfOfficeBalance = 0
                },
                new()
                {
                    FullName = "Полянська Федосія Янівна",
                    Subdivision = Subdivision.Development,
                    Position = Position.Developer,
                    Status = Status.Inactive,
                    OutOfOfficeBalance = 100
                }
            };

            foreach(var employee in employees)
            {
                await manager.AddEmployeeAsync(employee);
            }

            var dbEmployees = await manager.GetEmployeesAsync();

            var users = new User[]
            {
                new()
                {
                    EmployeeId = dbEmployees[0].Id,
                    Email = "hr@gmail.com",
                    Password = Sha256Helper.ComputeHash("admin"),
                },
                new()
                {
                    EmployeeId = dbEmployees[1].Id,
                    Email = "projectmanager@gmail.com",
                    Password = Sha256Helper.ComputeHash("admin"),
                },
                new()
                {
                    EmployeeId = dbEmployees[2].Id,
                    Email = "developer1@gmail.com",
                    Password = Sha256Helper.ComputeHash("admin"),
                },
                new()
                {
                    EmployeeId = dbEmployees[3].Id,
                    Email = "developer2@gmail.com",
                    Password = Sha256Helper.ComputeHash("admin"),
                },
                new()
                {
                    EmployeeId = dbEmployees[4].Id,
                    Email = "developer3@gmail.com",
                    Password = Sha256Helper.ComputeHash("admin"),
                },
            };
            
            foreach (var project in users)
            {
                await manager.AddUserAsync(project);
            }
            
            var projectManager = dbEmployees.FirstOrDefault(x=>x.Position == Position.ProjectManager);
            var projects = new AddEditProjectViewModel[]
            {
                new()
                {
                    Type = ProjectType.SoftwareDevelopment,
                    StartDate = new DateTime(2020,5, 25),
                    EndDate = new DateTime(2024,7, 1),
                    ProjectManagerId = projectManager.Id,
                    Status = Status.Inactive
                },
                new()
                {
                    Type = ProjectType.Research,
                    StartDate = new DateTime(2021,7, 15),
                    ProjectManagerId = projectManager.Id,
                    Status = Status.Active
                },
                new()
                {
                    Type = ProjectType.SoftwareDevelopment,
                    StartDate = new DateTime(2018,1, 10),
                    EndDate = new DateTime(2024,3, 11),
                    ProjectManagerId = projectManager.Id,
                    Status = Status.Inactive
                }
            };

            foreach (var project in projects)
            {
                await manager.AddProjectAsync(project);
            }
            
            var leaveRequests = new LeaveRequest[]
            {
                new()
                {
                    EmployeeId = dbEmployees[2].Id,
                    AbsenceReason = AbsenceReason.PersonalLeave,
                    StartDate = new DateTime(2020, 5, 2),
                    EndDate = new DateTime(2020, 5, 10),
                    Status = Status.Inactive
                },
                new()
                {
                    EmployeeId = dbEmployees[3].Id,
                    AbsenceReason = AbsenceReason.Vacation,
                    StartDate = new DateTime(2020, 7, 1),
                    EndDate = new DateTime(2020, 8, 1),
                    Status = Status.Active
                },
                new()
                {
                    EmployeeId = dbEmployees[2].Id,
                    AbsenceReason = AbsenceReason.SickLeave,
                    StartDate = new DateTime(2023, 2, 14),
                    EndDate = new DateTime(2023, 2, 27),
                    Status = Status.Active
                },
                new()
                {
                    EmployeeId = dbEmployees[3].Id,
                    AbsenceReason = AbsenceReason.Vacation,
                    StartDate = new DateTime(2022, 2, 8),
                    EndDate = new DateTime(2022, 2, 11),
                    Status = Status.Inactive
                },
                new()
                {
                    EmployeeId = dbEmployees[4].Id,
                    AbsenceReason = AbsenceReason.PersonalLeave,
                    StartDate = new DateTime(2024, 7, 5),
                    EndDate =new DateTime(2024, 8, 20),
                    Status = Status.Active
                }
            };

            foreach (var leaveRequest in leaveRequests)
            {
                await manager.AddLeaveRequestAsync(leaveRequest);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
