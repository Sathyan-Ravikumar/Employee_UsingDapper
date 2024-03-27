using Employee.Controllers;
using Employee.Requests;
using Employee.Service.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Controller_Test
{
    [TestClass]
    public class DepartmentController_Test
    {
        private  Mock<IDepartmentService> _departmentService;
        private DepartmentController _controller;
        public DepartmentController_Test() { 

            _departmentService = new Mock<IDepartmentService>();
            _controller = new DepartmentController(_departmentService.Object);
        }

        [TestMethod]
        public async Task AddDepartment_ReturnListOfAddedDepartment()
        {
            //assign
            List<DepartmentRequest> departmentRequests = new List<DepartmentRequest>()
            {
                new DepartmentRequest
                {
                    ID = 1,
                    DepartmentName = "Test"
                },
                new DepartmentRequest
                {
                    ID= 2,
                    DepartmentName= "Test1"
                },
                new DepartmentRequest { 
                    ID= 3,
                    DepartmentName = "Test3"
                }

            };

            //behaviour setup
            _departmentService.Setup(x => x.AddNewDepartment(departmentRequests)).ReturnsAsync(departmentRequests);

            //act
            var result = await _controller.AddDepartment(departmentRequests);

            //assert
            Assert.IsNotNull(result);
        }
    }
}
