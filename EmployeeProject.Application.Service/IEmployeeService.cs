using EmployeeProject.Domain.Entity;

namespace EmployeeProject.Application.Service
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeeList();
    }
}