using Company.Client.DAL.Common.Entities;

namespace Company.Client.DAL.Entities
{
    public class Department : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }

        //[InverseProperty(nameof(Employee.Department))]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

        public int ManagerId { get; set; }
        public virtual Employee Manager { get; set; }
    }
}
