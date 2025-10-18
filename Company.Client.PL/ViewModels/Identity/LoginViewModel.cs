using System.ComponentModel.DataAnnotations;

namespace Company.Client.PL.ViewModels.Identity
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is Required.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required.")]
        [DataType(DataType.Password)] // for encryption
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
