using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Client.PL.ViewModels.Identity
{
    public class SignUpViewModel
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is Required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username is Required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is Required.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required.")]
        [DataType(DataType.Password)] // for encryption
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required.")]
        [DataType(DataType.Password)]
        [Compare("Password" , ErrorMessage = "Password & ConfirmPassword do not Match")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
