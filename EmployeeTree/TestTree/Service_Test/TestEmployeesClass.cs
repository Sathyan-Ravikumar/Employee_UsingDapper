using AutoMapper;
using Employee.Model;
using Employee.Repository.Interface;
using Employee.Service;
using Employee.ViewModal;
using Moq;

namespace TestTree.Service_Test
{
    [TestClass]
    public class TestEmployeesClass
    {
        private Mock<IEmployeeRepository> _repoMock;
        private Mock<IMapper> _mapperMock;
        private EmployeeService _service;

        public TestEmployeesClass()
        {
            _repoMock = new Mock<IEmployeeRepository>();
            _mapperMock = new Mock<IMapper>();

            _service = new EmployeeService(_repoMock.Object, _mapperMock.Object);
        }
        [TestMethod]
        public async Task GetEmployees_ReturnsListOfEmployeesAsync()
        {
            //Assign
            List<EmployeesView> employeesViews = new List<EmployeesView>()
            {
                new EmployeesView()
                {
                    ID = 1,
                    EmployeeID = "EMP01",
                    FullName = "Foo Foo",
                    Position = "Lead"
                }
            };
            List<Employees> employees = new List<Employees>()
            {
                new Employees()
                {
                    ID = 1,
                    EmployeeID = "EMP01",
                    FirstName = "Foo",
                    LastName = "Foo",
                    Position = "Lead"
                }
            };


            //Behaviour Setup
            _repoMock.Setup(x => x.GetEmployees()).ReturnsAsync(employees);
            _mapperMock.Setup(m => m.Map<List<EmployeesView>>(employees)).Returns(employeesViews);

            //Act
            var result = await _service.GetEmployees();


            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
            Assert.IsTrue(result.Any());
            Assert.AreEqual(result[0].ID, 1);
            Assert.AreEqual(result[0].FullName, "Foo Foo");
        }

        [TestMethod]
        public async Task GetReporting_ReturnsListOfReportingForAllEmployees()
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
            _repoMock.Setup(x => x.GetReporting()).ReturnsAsync(emp);

            //act 
            var result = await _service.GetReporting();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetEmployeeByID_ReturnsEmployeeDetails()
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
            _repoMock.Setup(x => x.GetEmployeeByID(input)).ReturnsAsync(emp);

            //act
            var result = await _service.GetEmployeeByID(input);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(emp.EmployeeID, input);

        }

        [TestMethod]
        public async Task GetEmployee_ReturnsEmployeeDetails()
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
            _repoMock.Setup(x => x.GetEmployeeDepartmentByID(input)).ReturnsAsync(empDetails);

            //act
            var result = await _service.GetEmployee(input);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(empDetails.EmployeeID, input);

        }
    }
}