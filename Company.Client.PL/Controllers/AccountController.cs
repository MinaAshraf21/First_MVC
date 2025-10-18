using Company.Client.DAL.Entities.Identity;
using Company.Client.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Company.Client.BLL.Services.EmailSender;

namespace Company.Client.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager ,
                                SignInManager<ApplicationUser> signInManager ,
                                IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
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
            if (user is not null)
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
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            if(user is not null)
            {
                var passFlag = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passFlag)
                {
                    // isPersistent : indicates whether the authentication session is persisted
                    //          across multiple requests. When used with cookies,
                    //          a persistent cookie remains valid after the browser is closed.
                    // if true => cookie will be saved in hard disk , so even if browser is closed the cookie will be there
                    //              because the cookie will be loaded from the hard disk into cookie storage of browser

                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, isPersistent: loginVM.RememberMe, true);

                    if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError(string.Empty, "Account not Confirmed");
                        return View(loginVM);
                    }
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, $"Account is LockedOut for {user.LockoutEnd}");
                        return View(loginVM);
                    }

                    if (result.Succeeded)
                    {
                        user.EmailConfirmed = true;
                        return RedirectToAction("Index", "Home");
                    }

                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Login Attemp");
                        return View(loginVM);
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Email or Password");
            return View(loginVM);

        }

        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendResetPasswordUrl(ForgetPasswordViewModel forgetPasswordVM)
        {
            if (!ModelState.IsValid)
                return View("ForgetPassword", forgetPasswordVM);

            var user = _userManager.FindByEmailAsync(forgetPasswordVM.Email).Result;

            if (user is not null)
            {
                var _token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                //Request.Scheme => gets the scheme of the current request (HTTP or HTTPS) OR BaseURL
                var url = Url.Action("ResetPassword", "Account", new { email = forgetPasswordVM.Email, token = _token } , Request.Scheme);
                //Send Email with Reset Password Url
                var email = new Email()
                {
                    To = forgetPasswordVM.Email,
                    Subject = "Reset Your Password",
                    //BaseUrl/Account/ResetPassword?email=xxxxx&token=yyyy
                    //Body = URL ==> RESET PASSWORD FORM [pass & confirm pass]
                    //Body = "Click on this link to reset your password <a href='#'>Reset Password</a>"
                    Body = url
                };
                _emailSender.SendEmail(email);
                return RedirectToAction("CheckYourInbox");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Email Address");
                return View("ForgetPassword", forgetPasswordVM);
            }
        }

        [HttpGet]
        public IActionResult CheckYourInbox() { return View(); }

        [HttpGet]
        public IActionResult ResetPassword(string email , string token)
        {
            //pass email & token to the post action
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPasswordVM)
        {
            if(ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                var user = _userManager.FindByEmailAsync(email).Result;

                if (user != null)
                {
                    var result = _userManager.ResetPasswordAsync(user,token,resetPasswordVM.NewPassword).Result;

                    if (result.Succeeded)
                       return RedirectToAction("Login");
                }

            }
            ModelState.AddModelError("", "Invalid Operation , Try again later!");
            return View(resetPasswordVM);
        }
    }
}
