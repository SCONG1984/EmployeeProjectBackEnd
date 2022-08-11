using EmployeeProject.API.Controllers;
using EmployeeProject.Application.ExcelReader;
using EmployeeProject.Application.Service;
using EmployeeProject.Domain.Entity;
using EmployeeProject.Infrastructure.ExcelRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace EmployeeProject.UnitTest.API
{
    public class EmployeeControllerTest
    {
        ControllerBase _controller;

        [Fact]
        public async void Get_InvalidSource_Fail()
        {
            //Arrange
            var config = Options.Create(new ExcelConfig()
            {
                FileName = @"EmployeeData.csv"
            });

            var mockLoggerService = new Mock<ILogger<EmployeeService>>();
            var mockLoggerController = new Mock<ILogger<EmployeeController>>();
            var employeeRepository = new EmployeeRepository(config, new ExcelReader());
            var employeeService = new EmployeeService(employeeRepository, mockLoggerService.Object);

            var _controller = new EmployeeController(employeeService, mockLoggerController.Object);


            //Act
            Func<Task> act = () => _controller.Get();

            //Assert
            var exception = await Assert.ThrowsAsync<FileNotFoundException>(act);
        }

        [Fact]
        public async Task Get_ValidSource_Success()
        {
            //Arrange
            var config = Options.Create(new ExcelConfig()
            {
                FileName = @"EmployeeData.xlsx"
            });

            var mockLoggerService = new Mock<ILogger<EmployeeService>>();
            var mockLoggerController = new Mock<ILogger<EmployeeController>>();
            var employeeRepository = new EmployeeRepository(config, new ExcelReader());
            var employeeService = new EmployeeService(employeeRepository, mockLoggerService.Object);

            var _controller = new EmployeeController(employeeService, mockLoggerController.Object);

            //Act
            var act = await _controller.Get();
            var okAct = act as OkObjectResult;

            //Assert
            Assert.NotNull(act);
            Assert.Equal(200, okAct.StatusCode);
        }
    }
}
