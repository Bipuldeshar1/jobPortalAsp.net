using jobPortal.Models.ViewModel.Auth;
using Microsoft.AspNetCore.Mvc;

namespace jobPortal.Controllers
{
    public class AccountController : Controller
    {
      public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(registerViewModel model)
        {
            return View();
        }
    }
}
