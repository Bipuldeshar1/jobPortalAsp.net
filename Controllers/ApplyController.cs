using jobPortal.data;
using jobPortal.Models;
using jobPortal.Models.job;
using jobPortal.Models.ViewModel.apply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jobPortal.Controllers
{

    public class ApplyController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment enviroment;

        public ApplyController(UserManager<AppUser>userManager,ApplicationDbContext context,IWebHostEnvironment enviroment
            )
        {
            this.userManager = userManager;
            this.context = context;
            this.enviroment = enviroment;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ApplyJob(int id)
        {
            var job =  context.JobModels.Include(x=>x.appUser).FirstOrDefault(x => x.Id == id);
            ViewBag.JObId = job.Id;
            ViewBag.JObName = job.Title;
            ViewBag.info = job;
            var user= await userManager.GetUserAsync(User);
            ApplyViewModel applyViewModel = new ApplyViewModel() {
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                ApplyDate = DateTime.Now,
            };
          
            return View(applyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyJob(ApplyViewModel model)
        {
            var job = context.JobModels.Include(x => x.appUser).FirstOrDefault(x => x.Id == model.JobId);
            ViewBag.JObId = job.Id;
            ViewBag.JObName = job.Title;
            ViewBag.info = job;
            if (ModelState.IsValid)
            {
           
                if( model.Cl ==null || model.Cv == null)
                {
                    ModelState.AddModelError("","cv or cl not found");
                    return View(model);
                }

                var uploadCvFolder = Path.Combine(enviroment.WebRootPath,"cv");
                var uploadClFolder = Path.Combine(enviroment.WebRootPath, "cl");
              
                if (!Directory.Exists(uploadCvFolder))
                {
                    Directory.CreateDirectory(uploadCvFolder);
                }
                if (!Directory.Exists(uploadClFolder))
                {
                    Directory.CreateDirectory(uploadClFolder);
                }

                var fileNameCl= Guid.NewGuid().ToString()+Path.GetExtension(model.Cl.FileName);
                var fileNameCv = Guid.NewGuid().ToString() + Path.GetExtension(model.Cv.FileName);
               
                var filePathCv=Path.Combine(uploadCvFolder,fileNameCv);
                var filePathCl=Path.Combine(uploadClFolder,fileNameCl);
             
                using (var stream = new FileStream(filePathCv, FileMode.Create))
                {
                    await model.Cv.CopyToAsync(stream);
                }
                using (var stream = new FileStream(filePathCl, FileMode.Create))
                {
                    await model.Cl.CopyToAsync(stream);
                }


                ApplyModel applyModel = new ApplyModel() {
                    Name = model.Name,
                    Email = model.Email,
                    Address = model.Address,

                ApplyDate = DateTime.Now,
                JobId=model.JobId,
                Cv=filePathCv,
                Cl=fileNameCl,
                NameId=userManager.GetUserId(User),

                };
                await context.ApplyModels.AddAsync(applyModel);
                await context.SaveChangesAsync();
                return RedirectToAction("AppliedJob");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles ="Employeer")]
        public async Task<IActionResult> ApplicationReceived()
        {
            var user = await userManager.GetUserAsync(User);
       
            var jobs = await context.ApplyModels.Include(x => x.JobModel).ThenInclude(JobModel => JobModel.appUser).Where(x => x.JobModel.appUser.Id == user.Id).ToListAsync();
            return View(jobs);
        }

        public async Task<IActionResult> AppliedJob()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
               
                return RedirectToAction("Login", "Account");
            }

            var jobs = await context.ApplyModels
                                    .Include(x => x.appUser)
                                    .Include(x=>x.JobModel).ThenInclude(JobModel=>JobModel.appUser)
                                    .Where(x =>x.NameId  == user.Id)
                                    .ToListAsync();

            return View(jobs);
        }

    }
}
