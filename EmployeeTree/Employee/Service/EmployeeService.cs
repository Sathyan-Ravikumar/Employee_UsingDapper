using AutoMapper;
using Employee.Repository.Interface;
using Employee.Service.Interface;
using Employee.ViewModal;
using System.Collections.Generic;

namespace Employee.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository,IMapper mapper) 
        { 
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<List<EmployeesView>> GetEmployees()
        {
            var result = await _employeeRepository.GetEmployees();
            List<EmployeesView> emps = _mapper.Map<List<EmployeesView>>(result);
            return emps;
        }

        public async Task<List<ReportingForAllEmployees>> GetReporting()
        {
            return await _employeeRepository.GetReporting();
        }

        public async Task<ReportingForAllEmployees> GetEmployeeByID(string employeeID) 
        {
            var result = await _employeeRepository.GetEmployeeByID(employeeID);
            return result;
        }

        public  async Task<EmployeeDetail> GetEmployee(string employeeID)
        {
            return await _employeeRepository.GetEmployeeDepartmentByID(employeeID);
        }
     

    }
}
