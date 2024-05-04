using jobPortal.data;
using jobPortal.Models.category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jobPortal.Controllers
{
    
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext context;

        public CategoryController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category= await context.CategoriesModel.AddAsync(model);
                await context.SaveChangesAsync();
                return RedirectToAction("Read");

            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var category = context.CategoriesModel.ToList();
            return View(category);
        }



    }
}
