using Company.Client.DAL.Entities.Identity;
using Company.Client.PL.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Company.Client.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchTerm = "")
        {
            var userQuery = _userManager.Users;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.Trim().ToLower();
                userQuery = userQuery.Where(u =>
                    u.Email.ToLower().Contains(searchTerm) ||
                    u.FirstName.ToLower().Contains(searchTerm) ||
                    u.LastName.ToLower().Contains(searchTerm)
                );
            }

            // Get the actual user entities first
            var users = await userQuery.ToListAsync();

            // Create ViewModels and populate roles
            var viewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                viewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = roles.ToList()
                });
            }

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var userVM = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = await _userManager.GetRolesAsync(user)
            };
        
            return View(userVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var userVM = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = await _userManager.GetRolesAsync(user)
            };

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string? id , UserViewModel userVM)
        {
            if (!ModelState.IsValid)
                return View(userVM);

            if (string.IsNullOrEmpty(id))
                return BadRequest();

            if (id != userVM.Id)
                return BadRequest();

            string message = "User Updated Successfully!";

            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if(user == null)
                    return NotFound();

                user.FirstName = userVM.FirstName;
                user.LastName = userVM.LastName;
                user.Email = userVM.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    TempData["message"] = message;
                else
                    message = "Failed to Update user data!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                message = "Some Error occured , try again later!";
            }
            TempData["message"] = message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string? id)
        {

            if(string.IsNullOrEmpty(id))
                return BadRequest();

            string message = "User Deleted Successfully!";

            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                    return NotFound();

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                    TempData["message"] = message;
                else
                    message = "Failed to Delete user!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                message = "Some Error occured , try again later!";
            }
            TempData["message"] = message;
            return RedirectToAction("Index");
        }

    }
}
