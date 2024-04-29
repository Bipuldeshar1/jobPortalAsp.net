using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobPortal.Models.ViewModel.role
{
    public class RoleViewModel
    {
        [BindNever]
        public string Id { get; set; }
        [Required(ErrorMessage = "role name required")]
        public string RoleName { get; set; }
    }
}
