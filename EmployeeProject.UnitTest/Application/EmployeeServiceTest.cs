using EmployeeProject.Application.ExcelReader;
using EmployeeProject.Application.Service;
using EmployeeProject.Infrastructure.ExcelRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace EmployeeProject.UnitTest.Infrastructure
{
    public class EmployeeServiceTest
    {
        IEmployeeService _service;

        [Fact]
        public async void GetEmployeeList_Fail()
        {
            //Arrange
            var config = Options.Create(new ExcelConfig()
            {
                FileName = @"EmployeeData.csv"
            });

            var mock = new Mock<ILogger<EmployeeService>>();
            ILogger<EmployeeService> logger = mock.Object;

            _service = new EmployeeService(new EmployeeRepository(config, new ExcelReader()), logger);

            //Act
            Func<Task> act = () => _service.GetEmployeeList();

            //Assert
            var exception = await Assert.ThrowsAsync<FileNotFoundException>(act);
        }


        [Fact]
        public async void GetEmployeeList_Success()
        {
            var config = Options.Create(new ExcelConfig()
            {
                FileName = @"EmployeeData.xlsx"
            });

            var mock = new Mock<ILogger<EmployeeService>>();
            ILogger<EmployeeService> logger = mock.Object;

            _service = new EmployeeService(new EmployeeRepository(config, new ExcelReader()), logger);

            //Act
            var act = _service.GetEmployeeList();

            //Assert
            Assert.NotEmpty(act.Result);
        }
    }
}
