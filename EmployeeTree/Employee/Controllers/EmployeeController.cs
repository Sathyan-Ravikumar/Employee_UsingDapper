using Employee.Service.Interface;
using Employee.ViewModal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) {
           _employeeService = employeeService;
        }

        [HttpGet]

        public async Task<List<EmployeesView>> GetEmployee()
        {
            return await _employeeService.GetEmployees();
        }

        [HttpGet("Employee's-Reporting")]
        public async Task<List<ReportingForAllEmployees>> Reporting()
        {
            return await _employeeService.GetReporting();

        }

        [HttpGet("EmployeeByID")]

        public async Task<ReportingForAllEmployees> GetEmployee(string empID)
        {
            return await _employeeService.GetEmployeeByID(empID);
        }

        [HttpGet("Employee")]
        public  async Task<EmployeeDetail> GetEmpDepartByID(string employeeID)
        {
            return await _employeeService.GetEmployee(employeeID);
        }
    }
}
