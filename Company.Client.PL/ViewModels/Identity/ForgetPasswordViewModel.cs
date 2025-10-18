using System.ComponentModel.DataAnnotations;

namespace Company.Client.PL.ViewModels.Identity
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        public string Email { get; set; }
    }
}
