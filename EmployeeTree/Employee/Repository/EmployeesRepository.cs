using Dapper;
using Employee.Model;
using Employee.Model.Data;
using Employee.Repository.Interface;
using Employee.ViewModal;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Employee.Repository
{
    public class EmployeesRepository : IEmployeeRepository
    {
        private readonly ContextFile _context;
        public EmployeesRepository(ContextFile context) { 
            _context = context;
        }

        public async Task<List<Employees>> GetEmployees()
        {
            var query = "SELECT * FROM Employees";

            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Employees>(query);
                return companies.ToList();
            }
        }

        public async Task<List<ReportingForAllEmployees>> GetReporting()
        {
            var name = "EmployeeReporting";
            
            using(var connection = _context.CreateConnection())
            {
                var reporting = await connection.QueryAsync<ReportingForAllEmployees>(name, commandType: CommandType.StoredProcedure);
                return reporting.ToList();
            }
        }

        public async Task<ReportingForAllEmployees> GetEmployeeByID(string employeeID)
        {
            var spName = "EmployeeByID";
            var parameter = new DynamicParameters();
            parameter.Add("EmployeeID", employeeID);

            using (var connection = _context.CreateConnection())
            {
                var reporting = await connection.QueryFirstOrDefaultAsync<ReportingForAllEmployees>(
                    spName,
                    parameter,
                    commandType: CommandType.StoredProcedure
                );
                return reporting;
            }
        }

        public async Task<EmployeeDetail> GetEmployeeDepartmentByID(string employeeID)
        {
            var query = @"SELECT e.EmployeeID,
                            CONCAT(e.FirstName, ' ', e.LastName) AS FullName,
                            e.Position,
                            CASE
                                WHEN d.ID IS NULL THEN 'No Department'
                                ELSE d.DepartmentName
                                END AS DepartmentName
                            FROM Employees e
                            LEFT JOIN Department d ON e.DepartmentID = d.ID
                            WHERE e.EmployeeID = @EmployeeID
                            "; 

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<EmployeeDetail>(query, new { EmployeeID = employeeID });
                return result;
            }
        }


    }
}
