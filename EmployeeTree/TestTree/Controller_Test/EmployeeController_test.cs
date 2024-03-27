using Employee.Controllers;
using Employee.Service.Interface;
using Employee.ViewModal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Controller_Test
{
    [TestClass]
    public class EmployeeController_test
    {
        private Mock<IEmployeeService> _employeeServiceMock;
        private EmployeeController _controller;

        public EmployeeController_test()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            
            _controller = new EmployeeController( _employeeServiceMock.Object );
        }

        [TestMethod]
        public async Task GetEmployeecontroller_ReturnsListOfEmployees()
        {
            //Assign
            List<EmployeesView> employeesViews = new List<EmployeesView>()
            {
                new EmployeesView()
                {
                    ID = 1,
                    EmployeeID = "KA6",
                    FullName = "Foo Foo",
                    Position = "Lead"
                },
                new EmployeesView()
                {
                    ID = 2,
                    EmployeeID = "KA5",
                    FullName = "Rob Lucci",
                    Position = "Associate"
                }
            };

            //Behaviour Setup
            _employeeServiceMock.Setup(x => x.GetEmployees()).ReturnsAsync(employeesViews);

            //act
            var result = await _controller.GetEmployee();


            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public async Task Reporting_ReturnsReportingOfEmployees()
        {
            //Assign
            List<ReportingForAllEmployees> emp = new List<ReportingForAllEmployees>()
            {
                new ReportingForAllEmployees() {
                    EmployeeID = "KA2",
                    FullName = "Jane Smith",
                    EmployeePosition = "CTO",
                    DepartmentName = "IT Support",
                    ManagerFullName = "John Doe",
                    ManagerPosition = "CEO"
                },
                new ReportingForAllEmployees()
                {
                    EmployeeID = "KA3",
                    FullName = "Bob Johnson",
                    EmployeePosition = "CFO",
                    DepartmentName = "Software Development",
                    ManagerFullName = "John Doe",
                    ManagerPosition = "CEO"
                }
            };

            //Behaviour Setup
            _employeeServiceMock.Setup(x => x.GetReporting()).ReturnsAsync(emp);

            //act 
            var result = await _controller.Reporting();

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 2);

        }

        [TestMethod]
        public async Task GetEmployee_ReturnsAllEmployeeDetails()
        {
            //assign
            var emp = new ReportingForAllEmployees()
            {
                EmployeeID = "KA2",
                FullName = "Bob Johnson",
                EmployeePosition = "CFO",
                DepartmentName = "Software Development",
                ManagerFullName = "John Doe",
                ManagerPosition = "CEO"
            };

            var input = "KA2";

            //Behaviour Setup
            _employeeServiceMock.Setup(x => x.GetEmployeeByID(input)).ReturnsAsync(emp);

            //act 
            var result = await _controller.GetEmployee(input);

            //assert

            Assert.IsNotNull(result);
            Assert.AreEqual(input, result.EmployeeID,emp.EmployeeID);
        }

        [TestMethod]
        public async Task GetEmployee_RetunrsEmployeeDetails()
        {
            //assign
            var empDetails = new EmployeeDetail()
            {
                EmployeeID = "KA2",
                FullName = "Bob Johnson",
                Position = "CFO",
                DepartmentName = "Software Development"
            };
            var input = "KA2";

            //Behaviour Setup
            _employeeServiceMock.Setup(x => x.GetEmployee(input)).ReturnsAsync(empDetails);

            //act
            var result = await _controller.GetEmpDepartByID(input);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(empDetails.EmployeeID, input,result.EmployeeID);
        }
    }
}   
