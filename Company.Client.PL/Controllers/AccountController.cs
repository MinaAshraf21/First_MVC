using Company.Client.DAL.Entities.Identity;
using Company.Client.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Client.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpVM)
        {
            if (!ModelState.IsValid)
                return View(signUpVM);

            var user = await _userManager.FindByNameAsync(signUpVM.Username);
            if(user is not null)
            {
                ModelState.AddModelError("Username", "this UserName is already taken");
                return View(signUpVM);
            }
            user = new ApplicationUser()
               {
                    FirstName = signUpVM.FirstName,
                    LastName = signUpVM.LastName,
                    Email = signUpVM.Email,
                    IsAgree = signUpVM.IsAgree,
                    UserName = signUpVM.Username,
               };

            //creates user in the background [backing store OR Repository] 
            var result = await _userManager.CreateAsync(user, signUpVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(signUpVM);
            }
            return RedirectToAction(nameof(SignIn));
        }
    }
}
