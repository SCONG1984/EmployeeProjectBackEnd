using EmployeeProject.Application.ExcelReader;
using System.Data;

namespace EmployeeProject.UnitTest.Infrastructure
{
    public class ExcelReaderTest
    {
        IExcelReader _reader;

        [Fact]
        public void Read_InvalidSourceh_Fail()
        {
            //Arrange
            string fileName = @"EmployeeProject.API\\EmployeeData.xlsx";
            _reader = new ExcelReader();

            //Act
            Func<DataTable> act = () => _reader.Read(fileName);

            //Assert
            var exception = Assert.Throws<FileNotFoundException>(act);
        }

        [Fact]
        public void Read_InvalidSource_Success()
        {
            //Arrange
            string fileName = @"..\\..\\..\\..\\EmployeeProject.API\\EmployeeData.xlsx";
            _reader = new ExcelReader();

            //Act
            DataTable act = _reader.Read(fileName);

            //Assert
            Assert.NotNull(act);
        }
    }
}
