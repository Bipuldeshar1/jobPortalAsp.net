using Microsoft.AspNetCore.Mvc;

namespace jobPortal.Controllers
{
    public class JobController : Controller
    {
       public IActionResult Add()
        {
            return View();
        }
    }
}
