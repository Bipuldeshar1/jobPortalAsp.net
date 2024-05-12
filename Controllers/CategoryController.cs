using jobPortal.data;
using jobPortal.Models.category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jobPortal.Controllers
{
    
 
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext context;

        public CategoryController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel model)
        {
            ModelState.Remove("jobModels");
            if (ModelState.IsValid)
            {
                var category= await context.CategoriesModel.AddAsync(model);
                await context.SaveChangesAsync();
                return RedirectToAction("Read");

            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Read()
        {
            var category = context.CategoriesModel.ToList();
            return View(category);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = context.CategoriesModel.FirstOrDefault(x => x.Id==id);
            if (category == null) {
                ModelState.AddModelError("", "category not found");
                return RedirectToAction("Create");
            }
            context.CategoriesModel.Remove(category);
            await context.SaveChangesAsync();
            return RedirectToAction("Read");
        }

        [HttpGet]
        public async Task<IActionResult> CategoryJobDetail(int id)
        {
            var category= context.CategoriesModel.FirstOrDefault(x=>x.Id==id);
            if (category == null)
            {
                return View();
            }
            var jobs=  context.JobModels.Include(x=>x.appUser).Where(x=>x.Category.CategoryName==category.CategoryName).ToList();
            return View(jobs);
        }

    }
}
