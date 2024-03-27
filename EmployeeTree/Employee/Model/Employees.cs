namespace Employee.Model
{
    public class Employees
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int DepartmentID { get; set; }
        public int ManagerID { get; set; }
    }
}
