using Dapper;
using Employee.Model;
using Employee.Model.Data;
using Employee.Repository.Interface;
using System.Data;

namespace Employee.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ContextFile _context;
        public DepartmentRepository(ContextFile context) 
        {
            _context = context;
        }

        public async Task<Department> AddNewDepartment(Department department)
        {
            var query = @"INSERT INTO Department (DepartmentName)
                  OUTPUT INSERTED.ID, INSERTED.DepartmentName
                  VALUES (@DepartmentName);";

            using (var connection = _context.CreateConnection())
            {
                var parameters = new { DepartmentName = department.DepartmentName };
                var result = await connection.QueryFirstOrDefaultAsync<Department>(query, parameters);
                return result;
            }
        }


    }
}
