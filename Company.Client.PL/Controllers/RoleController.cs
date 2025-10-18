using Company.Client.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mealify.PL.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {

        RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveRole(RoleViewModel roleVM)
        {
            if (!ModelState.IsValid)
                return View(roleVM);

            var role = new IdentityRole
            {
                Name = roleVM.RoleName,
            };
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            foreach (var item in result.Errors)
                ModelState.AddModelError(string.Empty, item.Description);
            return View(roleVM);
        }
    }
}
