using jobPortal.data;
using jobPortal.Models.ViewModel.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace jobPortal.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext context;

        public SearchController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> SearchJob(string? JobTitle, string? CompanyName)
        {
            if (!string.IsNullOrEmpty(JobTitle) && string.IsNullOrEmpty(CompanyName))
            {
                var jobs = context.JobModels.Where(x => x.Title.Contains(JobTitle)).ToList();
                return View(jobs);
            }
            else if (!string.IsNullOrEmpty(CompanyName) && string.IsNullOrEmpty(JobTitle))
            {
                var companies = context.AppUsers.Where(x => x.Name.Contains(CompanyName)).ToList();
                return View("CompanySearch",companies);
            }
            else if (!string.IsNullOrEmpty(JobTitle) || !string.IsNullOrEmpty(CompanyName))
            {
                var jobs = context.JobModels.Include(x => x.appUser).AsQueryable();

                if (!string.IsNullOrEmpty(CompanyName))
                {
                    jobs = jobs.Where(x => x.appUser.Name.Contains(CompanyName));
                }

                if (!string.IsNullOrEmpty(JobTitle))
                {
                    jobs = jobs.Where(x => x.Title.Contains(JobTitle));
                }

                return View(await jobs.ToListAsync());
            }

            return View();
        }

    }
}
