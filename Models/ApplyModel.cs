using jobPortal.Models.job;
using System.ComponentModel.DataAnnotations;

namespace jobPortal.Models
{
    public class ApplyModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime ApplyDate { get; set; }
        public string Cv { get; set; }

        public string Cl{get; set;}

        public int JobId { get; set; }
        public JobModel JobModel { get; set; }

    
    }
}
