using Azure.Messaging.ServiceBus;
using Employee.Model;
using Employee.Requests;
using Employee.Service.Interface;
using Employee.Service.NonActionMethod;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly INonAction _nonAction;
        public DepartmentController(IDepartmentService departmentService,INonAction nonAction)
        {
            _departmentService = departmentService;
            _nonAction = nonAction;
        }

        [HttpPost]
        public async Task<List<DepartmentRequest>> AddDepartment(List<DepartmentRequest> department)
        {
            var result = await _departmentService.AddNewDepartment(department);
            await _nonAction.DepartServiceBus(result); 
            return result;
        }
        

    }
}
