using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobPortal.Models.ViewModel.role
{
    public class RoleViewModel
    {
       

        [Required(ErrorMessage = "role name required")]
        public string RoleName { get; set; }
    }
}
