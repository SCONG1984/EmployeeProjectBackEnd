using System.Data;

namespace EmployeeProject.Application.ExcelReader
{
    public interface IExcelReader
    {
        DataTable Read(string fileName);
    }
}