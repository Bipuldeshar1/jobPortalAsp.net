using jobPortal.data;
using jobPortal.Models;
using jobPortal.Models.job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employeer")]
        public async Task<IActionResult> Add(AddJobModel model)
        {
            if (ModelState.IsValid)
            {

               
                JobModel jobModel = new JobModel()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Resposibility = model.Resposibility,
                    Salary=model.Salary,
                    AuthorId= userManager.GetUserId(User),
                   
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
    }
}
