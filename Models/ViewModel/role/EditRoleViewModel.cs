using System.ComponentModel.DataAnnotations;

namespace jobPortal.Models.ViewModel.role
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "role name required")]
        public string RoleName { get; set; }
    }
}
