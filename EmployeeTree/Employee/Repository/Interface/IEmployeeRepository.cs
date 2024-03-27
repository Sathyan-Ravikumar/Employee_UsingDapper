using Employee.Model;
using Employee.ViewModal;

namespace Employee.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Task<List<Employees>> GetEmployees();
        Task<List<ReportingForAllEmployees>> GetReporting();
        Task<ReportingForAllEmployees> GetEmployeeByID(string employeeID);
        Task<EmployeeDetail> GetEmployeeDepartmentByID(string employeeID);
    }
}
