using Employee.Model;
using Employee.Requests;

namespace Employee.Service.Interface
{
    public interface IDepartmentService
    {
        Task<List<DepartmentRequest>> AddNewDepartment(List<DepartmentRequest> department);
    }
}
