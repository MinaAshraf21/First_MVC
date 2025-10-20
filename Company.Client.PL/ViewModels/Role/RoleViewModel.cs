using System.ComponentModel.DataAnnotations;

namespace Company.Client.PL.ViewModels.Role
{
    public class RoleViewModel
    {
        [Display(Name = "Role Id")]
        public string? Id { get; set; }
        [Display(Name = "Role Name")]
        public string Name { get; set; }

        public List<UserRoleViewModel> Users { get; set; } = new List<UserRoleViewModel>();
    }
}
