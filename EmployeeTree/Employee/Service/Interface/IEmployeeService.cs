using Employee.ViewModal;

namespace Employee.Service.Interface
{
    public interface IEmployeeService
    {
        Task<List<EmployeesView>> GetEmployees();
        Task<List<ReportingForAllEmployees>> GetReporting();
        Task<ReportingForAllEmployees> GetEmployeeByID(string employeeID);
        Task<EmployeeDetail> GetEmployee(string employeeID);
    }
}
