using EmployeeProject.Domain.Entity;
using EmployeeProject.Domain.Repository;
using Microsoft.Extensions.Logging;

namespace EmployeeProject.Application.Service
{
    public class EmployeeService : IEmployeeService
    {
        public IRepository<Employee> employeeRepository;
        private readonly ILogger<EmployeeService> logger;

        public EmployeeService(IRepository<Employee> employeeRepository, ILogger<EmployeeService> logger)
        {
            this.employeeRepository = employeeRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeList()
        {
            var employeeList = await employeeRepository.GetAll();
            return employeeList;
        }

    }
}
