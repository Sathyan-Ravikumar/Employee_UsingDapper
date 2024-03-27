using Employee.Model;
using Employee.Requests;
using Employee.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        public async Task<List<DepartmentRequest>> AddDepartment(List<DepartmentRequest> department)
        {
            var result = await _departmentService.AddNewDepartment(department);
            return result;
        }
    }
}
