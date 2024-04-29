using jobPortal.Migrations;
using jobPortal.Models;
using jobPortal.Models.ViewModel.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace jobPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(RoleManager<IdentityRole>roleManager,UserManager<AppUser>userManager,SignInManager<AppUser>signInManager)
        {
           this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        public async Task<IActionResult> Register()
        {
            var roles = await roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.RoleList = new SelectList(roles);
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Register(registerViewModel model)
        {
            var user = new AppUser
            {
                UserName=model.Name,
                Email=model.Email,
                Name=model.Name,
                Address=model.Address
            };
            if (ModelState.IsValid)
            {
                var result = await userManager.CreateAsync(user,model.Password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, model.Role);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model,string? returnUrl)
        {
            if(ModelState.IsValid)
            {
                var user=await userManager.FindByEmailAsync(model.Email);
                if (user ==null)
                {
                    return RedirectToAction("Login");
                }
                var result = await signInManager.PasswordSignInAsync(user.UserName,model.Password,false,false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "incorrect login cred");
                return View(model);
            }
            return View(model);
        }
    }


    
}

