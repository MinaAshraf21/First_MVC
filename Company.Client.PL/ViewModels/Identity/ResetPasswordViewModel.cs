using System.ComponentModel.DataAnnotations;

namespace Company.Client.PL.ViewModels.Identity
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password Field is Required!!")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password Field is Required!!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare(nameof(NewPassword),ErrorMessage = "Password & ConfirmPassword do not Match")]
        public string ConfirmNewPassword { get; set; }

    }
}
