using System.ComponentModel.DataAnnotations;

namespace Company.Client.PL.ViewModels.Identity
{
    public class RoleViewModel
    {
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
