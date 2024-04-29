using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace jobPortal.Models
{
    public class AppUser:IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public string type { get; set; }


    }
}
