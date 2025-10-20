using System.ComponentModel.DataAnnotations;

namespace Company.Client.PL.ViewModels.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "First Name is Required!!")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required!!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is Required!!")]
        [EmailAddress]
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
