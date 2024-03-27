using AutoMapper;
using Employee.Model;
using Employee.Repository.Interface;
using Employee.Requests;
using Employee.Service.Interface;

namespace Employee.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentService(IDepartmentRepository departmentRepository,IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;

        }

        public async Task<List<DepartmentRequest>> AddNewDepartment(List<DepartmentRequest> departments)
        {
            var results = new List<DepartmentRequest>();

            var departConvert = _mapper.Map<List<Department>>(departments);
            foreach (var departmentRequest in departConvert)
            {
                var result = await _departmentRepository.AddNewDepartment(departmentRequest);
                if (result != null)
                {
                    var departModel = _mapper.Map<DepartmentRequest>(result);
                    results.Add(departModel);
                } 
            }
            return results;
        }
    }
}
