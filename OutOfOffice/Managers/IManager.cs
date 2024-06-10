using OutOfOffice.Models;

namespace OutOfOffice.Managers
{
    public interface IManager
    {
        Task<List<Employee>> GetEmployeesAsync();
    }
}