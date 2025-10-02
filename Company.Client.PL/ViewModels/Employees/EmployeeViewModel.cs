using Company.Client.DAL.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Client.PL.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [DisplayName("Hiring Date")]
        public string? FormattedHireDate { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [DisplayName("Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string? DepartmentName { get; set; }

        #region Adminstration
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        #endregion


    }
}