using Company.Client.DAL.Common.Entities;
using Company.Client.DAL.Common.Enums;

namespace Company.Client.DAL.Entities
{
    public class Employee : BaseAuditableEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        //public double Salary { get; set; } // float in Database
        public decimal Salary { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; } //will be stored as an int , so we will modify that in EmpConfigs
        public EmployeeType EmployeeType { get; set; }

        public string? ImagePath { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual Department? ManagedDepartment { get; set; }
    }
}
