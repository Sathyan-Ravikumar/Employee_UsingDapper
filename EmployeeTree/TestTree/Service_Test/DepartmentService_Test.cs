using AutoMapper;
using Employee.Controllers;
using Employee.Model;
using Employee.Repository.Interface;
using Employee.Requests;
using Employee.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Service_Test
{
    [TestClass]
    public class DepartmentService_Test
    {
        private Mock<IDepartmentRepository> _departmentRepo;
        private Mock<IMapper> _mapper;
        private DepartmentService _departService;
        public DepartmentService_Test() 
        {
            _departmentRepo = new Mock<IDepartmentRepository>();
            _mapper = new Mock<IMapper>();
            _departService = new DepartmentService(_departmentRepo.Object,_mapper.Object);

        }

        [TestMethod]
        public async Task AddNewDepartment_ReturnsMappedDepartments()
        {
            // Arrange
            var departments = new List<DepartmentRequest>
            {
                new DepartmentRequest { ID = 1, DepartmentName = "Dept1" },
                new DepartmentRequest { ID = 2,DepartmentName = "Dept2" }
            };
            var departmentModels = new List<Department>
            {
                new Department { ID = 1, DepartmentName = "Dept1" },
                new Department { ID = 2, DepartmentName = "Dept2" }
            };
            /*var mappedDepartments = new List<DepartmentRequest>
            {
                new DepartmentRequest { ID = 1, DepartmentName = "Dept1" },
                new DepartmentRequest { ID = 2, DepartmentName = "Dept2" }
            };*/
            
            //behaviour setup

            _mapper.Setup(m => m.Map<List<Department>>(departments)).Returns(departmentModels);
            foreach (var department in departmentModels)
            {
                _departmentRepo.Setup(r => r.AddNewDepartment(department))
                               .ReturnsAsync(department);
            }


            // Act
            var result = await _departService.AddNewDepartment(departments);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(departments.Count, result.Count);
            

        }
    }
}
