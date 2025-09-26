using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Client.PL.ViewModels.Departments
{
    public class CreateDepartmentViewModel
    {
        [Required]
        public required string Name { get; set; }
        [Required(ErrorMessage ="The Code Field is Required (:")]
        public required string Code { get; set; }
        [DisplayName("Creation Date")]
        public DateOnly CreationDate { get; set; }
        public string? Description { get; set; }

    }
}
