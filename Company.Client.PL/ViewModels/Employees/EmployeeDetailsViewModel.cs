using Company.Client.DAL.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Client.PL.ViewModels.Employees
{
    public class EmployeeDetailsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        [DisplayName("Hiring Date")]
        public string? FormattedHireDate { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [DisplayName("Status")]
        public bool IsActive { get; set; }
        public string Status => IsActive? "Actvie" : "InActive";
        public int YearsOfService { get; set; }
        public int Age { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [DisplayName("Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [DisplayName("Department Name")]
        public string DepartmentName { get; set; }
        [DisplayName("Department Id")]
        public int DepartmentId { get; set; }
        [DisplayName("Department Code")]
        public string DepartmentCode { get; set; }
        [DisplayName("Department Description")]
        public string DepartmentDescription { get; set; }
        [DisplayName("ManagedDepartment Name")]
        public string? ManagedDepartmentName { get; set; }
        [DisplayName("ManagedDepartment Id")]
        public int? ManagedDepartmentId { get; set; }
        public string? DepartmentManagerName { get; set; }


        #region Adminstration
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        #endregion


    }
}