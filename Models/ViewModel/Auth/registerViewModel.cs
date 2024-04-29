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
    }
}
