using jobPortal.Models.job;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace jobPortal.Models
{
    public class AppUser:IdentityUser
    {
      
        public string Name { get; set; }
       
        public string Address { get; set; }
        public string type { get; set; }

        public IEnumerable<JobModel> JobModels { get; set; }


    }
}
