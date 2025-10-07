using Company.Client.DAL.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Client.PL.ViewModels.Employees
{
    public class UpdateEmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        [MinLength(3,ErrorMessage = "Minumim Length of First Name is 5 Charachters")]
        [MaxLength(25,ErrorMessage = "Maximum Length of First Name is 25 Charachters")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        [MinLength(3, ErrorMessage = "Minumim Length of Last Name is 5 Charachters")]
        [MaxLength(25, ErrorMessage = "Maximum Length of Last Name is 25 Charachters")]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,20}-[a-zA-Z]{5,20}-[a-zA-Z]{5,20}$",
                                    ErrorMessage = "Address must be Like 123-Street-City-Country")]
        public string? Address { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01,100000,ErrorMessage = "Salary must be in Range (0.01 , 100000)")]
        public decimal Salary { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        public string Status => IsActive? "Actvie" : "InActive";
        //[Required]
        //public int Age { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [DisplayName("Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; } = new List<SelectListItem>();



    }
}