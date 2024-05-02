namespace jobPortal.Models.ViewModel.apply
{
    public class ApplyViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime ApplyDate { get; set; }
        public IFormFile Cv { get; set; }

        public IFormFile Cl { get; set; }

        public int JobId { get; set; }
  

    }
}
