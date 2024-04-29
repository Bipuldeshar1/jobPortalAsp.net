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

        public AccountController(RoleManager<IdentityRole>roleManager,UserManager<AppUser>userManager)
        {
           this.roleManager = roleManager;
            this.userManager = userManager;
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
    }


    
}

