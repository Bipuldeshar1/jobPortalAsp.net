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

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var category = context.CategoriesModel.ToList();
            return View(category);
        }

        [HttpGet]
        public  IActionResult Edit(int id)
        {
            var category =  context.CategoriesModel.FirstOrDefault(x=>x.Id==id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category =  context.CategoriesModel.FirstOrDefault(x => x.Id == model.Id);
                if (category == null)
                {
                    ModelState.AddModelError("", "category not found");
                    return View(model);
                }
                category.CategoryName=model.CategoryName;
                await context.SaveChangesAsync();
                return RedirectToAction("Read");
            }

            return View(model);
        }



    }
}
