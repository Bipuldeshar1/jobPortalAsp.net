using System.ComponentModel.DataAnnotations;

namespace jobPortal.Models.job
{
    public class JobModel
    {
        [Key]
        public int Id { get; set;}

        public string Title { get; set;}
        public string Description { get; set;}
        public string Resposibility { get; set;}
        public DateTime PostedAt { get; set; } = DateTime.Now;        
        public string Salary { get; set;}

        public string AuthorId { get; set;}
        public AppUser appUser { get; set;}
        public IEnumerable<ApplyModel> applyModels { get; set; }

    }
}
