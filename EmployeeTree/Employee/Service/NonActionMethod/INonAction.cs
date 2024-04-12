using Employee.Requests;

namespace Employee.Service.NonActionMethod
{
    public interface INonAction
    {
        Task DepartServiceBus(List<DepartmentRequest> result);
    }
}
