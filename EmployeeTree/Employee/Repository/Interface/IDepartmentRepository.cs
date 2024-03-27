using Employee.Model;

namespace Employee.Repository.Interface
{
    public interface IDepartmentRepository
    {
        Task<Department> AddNewDepartment(Department department);
    }
}
