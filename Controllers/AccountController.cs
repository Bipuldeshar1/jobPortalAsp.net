
using jobPortal.Models;
using jobPortal.Models.ViewModel.Auth;
using jobPortal.services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace jobPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IImageService imageService;

        public AccountController(RoleManager<IdentityRole>roleManager,UserManager<AppUser>userManager,
            SignInManager<AppUser>signInManager,IImageService imageService)
        {
           this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.imageService = imageService;
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


           
            if (ModelState.IsValid)
            {
                string ImageUrl = "";
             if(model.image!=null && model.image.Length > 0)
                {
                    ImageUrl= await imageService.uploadImageAsync(model.image);
                }
                var user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                    Name = model.Name,
                    Address = model.Address


                };
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
                    ModelState.AddModelError("" , "user not found");
                    return View(model);
                }
                var result = await signInManager.PasswordSignInAsync(user.UserName,model.Password,false,false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        ModelState.AddModelError("","user not found");
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "incorrect login cred");
                return View(model);
            }
            return View(model);
        }

        public  async Task<IActionResult> Logout()
        {
           await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UserDetail()
        {
            var userId =  userManager.GetUserId(User);

            var user= await userManager.FindByIdAsync(userId);

            var roles = await userManager.GetRolesAsync(user);

            UserDetailViewModel userDetailViewModel = new UserDetailViewModel() {
                Id = userId,
                UserName = user.UserName,
                Email = user.Email,
                Address=user.Address,
                role = roles.FirstOrDefault()


            };
          
            return View(userDetailViewModel);
        }
    }


    
}

