using EmployeeProject.Application.ExcelReader;
using EmployeeProject.Application.Service;
using EmployeeProject.Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;

namespace EmployeeProject.Infrastructure.ExcelRepository
{
    public abstract class ExcelBaseRepository<T> : IRepository<T>
    {
        protected readonly string FileFullName = string.Empty;
        protected readonly IExcelReader ExcelReader;

        public ExcelBaseRepository(IOptions<ExcelConfig> config, IExcelReader excelReader)
        {
            FileFullName = config.Value.FileName;
            ExcelReader = excelReader;
        }

        public abstract Task<IEnumerable<T>> GetAll();
    }
}
