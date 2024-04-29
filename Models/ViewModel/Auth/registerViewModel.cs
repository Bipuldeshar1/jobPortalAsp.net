using jobPortal.Models.ViewModel.role;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace jobPortal.Models.ViewModel.Auth
{
    public class registerViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string cPassword { get; set; }
        [Required]
        public string Address { get; set; }      
        public string Role { get; set; }
     
    }
}
