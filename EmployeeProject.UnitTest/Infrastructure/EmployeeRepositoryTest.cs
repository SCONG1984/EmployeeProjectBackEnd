using EmployeeProject.Application.ExcelReader;
using EmployeeProject.Application.Service;
using EmployeeProject.Domain.Entity;
using EmployeeProject.Domain.Repository;
using EmployeeProject.Infrastructure.ExcelRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EmployeeProject.UnitTest.Domain
{
    public class EmployeeRepositoryTest
    {
        private IRepository<Employee> _repo;

        [Fact]
        public async void GetAll_InvalidSource_Fail()
        {
            //Arrange
            var config = Options.Create(new ExcelConfig()
            {
                FileName = @"EmployeeData.csv"
            });

            _repo = new EmployeeRepository(config, new ExcelReader());

            //Act
            Func<Task> act = () => _repo.GetAll();

            //Assert
            var exception = await Assert.ThrowsAsync<FileNotFoundException>(act);            
        }

        [Fact]
        public async void GetAll_ValidSource_Success()
        {
            //Arrange
            var config = Options.Create(new ExcelConfig()
            {
                FileName = @"EmployeeData.xlsx"
            });

            _repo = new EmployeeRepository(config, new ExcelReader());

            //Act
            var act = await _repo.GetAll();

            //Assert
            Assert.Equal(10, act.Count());
        }

    }
}