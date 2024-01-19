using HomeTaskkMVC4.Helpers;
using HomeTaskkMVC4.Models;
using HomeTaskkMVC4.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HomeTaskkMVC4.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager) 
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)

        {
            if (!ModelState.IsValid) return View();
            AppUser appUser = new AppUser() ;
            appUser.FullName = registerVM.FullName;
            appUser.Email = registerVM.Email;
            appUser.UserName = registerVM.UserName;

      IdentityResult result=  await _userManager.CreateAsync(appUser,registerVM.Password);
            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }

            await _userManager.AddToRoleAsync(appUser,RoleEnum.Admin.ToString());
            return RedirectToAction("login");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Login ()
        {
             
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM,string ReturnUrl)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "UserNameOrEmail or Password Sehvdir");
                    return View(loginVM);
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Hesabiniz Bloklanib");
                return View(loginVM); 
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "usernameoremail ve ya password sehvdir");
                return View(loginVM);
            }

            await _signInManager.SignInAsync(user, loginVM.RememberMe);
            if(ReturnUrl!=null)
            {
                return Redirect(ReturnUrl);
            }

            var roles= await _userManager.GetRolesAsync(user);
            foreach(var item in roles)
            {
                if(item=="Admim")
                {
                    return RedirectToAction("index", "dashboard",new {area="AdminArea"});
                }
            }

            return RedirectToAction("index","home");
        }
    }
}
