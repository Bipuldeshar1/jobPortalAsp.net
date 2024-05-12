using jobPortal.Models;
using jobPortal.Models.ViewModel.role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace jobPortal.Controllers
{

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public RoleController(RoleManager<IdentityRole>roleManager,UserManager<AppUser>userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }


        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole() {
                    Name = model.RoleName,
                };

                var result=await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public  async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("ListRoles", "Role");
            }
            var model = new EditRoleViewModel { 
            
            Id=role.Id,
            RoleName=role.Name,
            };
          

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                return RedirectToAction("ListRoles", "Role");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var e in result.Errors)
                {
                    ModelState.AddModelError("", e.Description);
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult>DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("ListRoles");
            }
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction("ListRoles");
            }
            
        }

    }
}
