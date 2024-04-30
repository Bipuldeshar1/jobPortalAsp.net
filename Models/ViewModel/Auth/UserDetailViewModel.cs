using jobPortal.Models.ViewModel.role;

namespace jobPortal.Models.ViewModel.Auth
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string role { get; set; }
    }
}
