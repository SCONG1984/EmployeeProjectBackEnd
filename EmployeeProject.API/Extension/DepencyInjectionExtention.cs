using EmployeeProject.Application.ExcelReader;
using EmployeeProject.Application.Service;
using EmployeeProject.Domain.Entity;
using EmployeeProject.Domain.Repository;
using EmployeeProject.Infrastructure.ExcelRepository;

namespace EmployeeProject.API.Extension
{
    public static class DepencyInjectionExtention
    {
        public static void RegisterDI(this IServiceCollection serviceProvider, IConfiguration config)
        {
            serviceProvider.AddScoped<IExcelReader, ExcelReader>();
            serviceProvider.AddScoped<IRepository<Employee>, EmployeeRepository>();
            serviceProvider.AddScoped<IEmployeeService, EmployeeService>();
            serviceProvider.Configure<ExcelConfig>(config.GetSection("ExcelConfig"));
        }
    }
}
