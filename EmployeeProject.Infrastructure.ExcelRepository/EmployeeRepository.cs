using EmployeeProject.Application.ExcelReader;
using EmployeeProject.Application.Service;
using EmployeeProject.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;

namespace EmployeeProject.Infrastructure.ExcelRepository
{
    public class EmployeeRepository : ExcelBaseRepository<Employee>
    {
        public EmployeeRepository(IOptions<ExcelConfig> config, IExcelReader excelReader) : base(config, excelReader)
        {

        }

        public override async Task<IEnumerable<Employee>> GetAll()
        {
            var employeeList = new List<Employee>();
            
            await Task.Run(() =>
            {
                var dtEmployee = ExcelReader.Read(FileFullName);
                var employee = new Employee();
                foreach (DataRow row in dtEmployee.Rows)
                {
                    employee = new Employee
                    {
                        EmployeeNumber = (string)row[0],
                        FirstName = (string)row[1],
                        LastName = (string)row[2],
                        Status = (string)row[3]
                    };

                    employeeList.Add(employee);
                }
            });

            return employeeList;
        }
    }
}