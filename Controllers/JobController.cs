using jobPortal.data;
using jobPortal.Models;
using jobPortal.Models.job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace jobPortal.Controllers
{
    
    public class JobController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ApplicationDbContext context;

        public JobController(UserManager<AppUser>userManager,SignInManager<AppUser>signInManager,ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Employeer")]
        public async Task<IActionResult> Add()
        {
            var category =  context.CategoriesModel.Select(x=>x.CategoryName).ToList();
            ViewBag.Category = new SelectList(category);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employeer")]
        public async Task<IActionResult> Add(AddJobModel model)
        {
            if (ModelState.IsValid)
            {
                var category = context.CategoriesModel.ToList();
                ViewBag.Category = category;

                var cat =  context.CategoriesModel.Where(x=>x.CategoryName==model.Category).FirstOrDefault();


                JobModel jobModel = new JobModel()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Resposibility = model.Resposibility,
                    Salary=model.Salary,
                    AuthorId= userManager.GetUserId(User),
                    CategoryId= cat.Id,
                   
                };
                await context.JobModels.AddAsync(jobModel);
                await context.SaveChangesAsync();
                return RedirectToAction("Read");
        
                
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var jobs =  context.JobModels.Include(x=>x.appUser).ToList();

            return View(jobs);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var job =  context.JobModels.Include(x=>x.appUser).Include(x=>x.Category).FirstOrDefault(x=>x.Id==id);
            return View(job);
        }

        [HttpGet]
        [Authorize(Roles ="Employeer")]
        public async Task<IActionResult> GetMyJobsPost()
        {
            var user= await userManager.GetUserAsync(User);

            var jobs = context.JobModels.Include(x => x.appUser).Where(x=>x.appUser.Id==user.Id).ToList();

          

            return View(jobs);
        }
        [HttpGet]
        [Authorize(Roles = "Employeer")]
        public async Task<IActionResult> Edit(int id)
        {
           
            var category = context.CategoriesModel.Select(x=>x.CategoryName).ToList();
            ViewBag.Category = new SelectList(category);
            var job= await context.JobModels.Include(x=>x.appUser).FirstOrDefaultAsync(x=>x.Id==id);
      
            ViewBag.AlraeadyCat = new SelectList(category);


            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(JobModel model)
        {
            
            var userID = userManager.GetUserId(User);
                var job =  context.JobModels.Include(x=>x.appUser).FirstOrDefault(x=>x.Id==model.Id);

           var cat = context.CategoriesModel.Where(x=>x.CategoryName==model.CategoryName).FirstOrDefault();

            if (job.appUser.Id == userID)
            {
                job.Title = model.Title;
                job.Description = model.Description;
                job.Resposibility = model.Resposibility;
                job.Salary = model.Salary;
                job.CategoryId = cat.Id;
                await context.SaveChangesAsync();
                return RedirectToAction("Read", "Job");
            }
            else
            {
                return View(model);
            }
        }

 
        public async Task<IActionResult> Delete(int id)
        {
            var job= await context.JobModels.FindAsync(id);
            if(job == null)
            {
                return RedirectToAction("Read");
            }
             context.JobModels.Remove(job);
            await context.SaveChangesAsync();
            return RedirectToAction("Read");

        }
    }
}
