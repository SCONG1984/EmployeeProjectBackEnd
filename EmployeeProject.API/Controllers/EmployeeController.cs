using EmployeeProject.Application.Service;
using EmployeeProject.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            this.employeeService = employeeService;

        }
        
        [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
        [HttpGet]        
        public async Task<IActionResult> Get()
        {
            var employeeList = await employeeService.GetEmployeeList();
            return Ok(employeeList);
        }
    }
}
