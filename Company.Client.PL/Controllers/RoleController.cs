using Company.Client.DAL.Entities.Identity;
using Company.Client.PL.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mealify.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {

        RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager ,
                              IWebHostEnvironment webHostEnvironment ,
                              UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _environment = webHostEnvironment;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;

            var rolesVM = roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return View(rolesVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
                return NotFound();

            var roleVM = new RoleViewModel
            {
                Id = id,
                Name = role.Name
            };

            return View(roleVM);
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
                return View("AddRole",roleVM);

            var message = "Role Added Successfully!";
            var role = new IdentityRole
            {
                Name = roleVM.Name,
            };
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                message = "Failed to Add Role!";
                foreach (var item in result.Errors)
                    ModelState.AddModelError(string.Empty, item.Description);
                return View("AddRole", roleVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
                return NotFound();

            var users = await _userManager.Users.ToListAsync();

            var roleVM = new RoleViewModel
            {
                Id = id,
                Name = role.Name,
                Users = users.Select(u => new UserRoleViewModel
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    IsSelected = _userManager.IsInRoleAsync(u , role.Name).Result
                }).ToList()
            };

            return View(roleVM);

        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]string id ,RoleViewModel roleVM)
        {
            if (!ModelState.IsValid)
                return View(roleVM);

            if(id != roleVM.Id)
                return BadRequest();

            string message = "User Updated Successfully!";

            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null)
                    return NotFound();

                role.Name = roleVM.Name;

                var result = await _roleManager.UpdateAsync(role);

                foreach (var userRoleVM in roleVM.Users)
                {

                    var user = await _userManager.FindByIdAsync(userRoleVM.UserId);
                    if(user is not null)
                    {

                        if(userRoleVM.IsSelected && !(await _userManager.IsInRoleAsync(user , role.Name)))
                        {
                            await _userManager.AddToRoleAsync(user, role.Name);
                        }
                        else if(!userRoleVM.IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(user, role.Name);
                        }

                    }

                }


                if (result.Succeeded)
                {
                    TempData["message"] = message;
                    return RedirectToAction("Index");
                }
                else
                {
                    message = "Failed to Update Role!";
                    foreach (var item in result.Errors)
                        ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "an error occured , try again later!";
            }
            TempData["message"] = message;
            return View(roleVM);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(string? id)
        {

            string message = "User Deleted Successfully!";

            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null)
                    return NotFound();

                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    TempData["message"] = message;
                }
                else
                {
                    message = "Failed to Delete Role!";
                    foreach (var item in result.Errors)
                        ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "an error occured , try again later!";
            }
            TempData["message"] = message;
            return RedirectToAction("Index");
        }

    }
}
