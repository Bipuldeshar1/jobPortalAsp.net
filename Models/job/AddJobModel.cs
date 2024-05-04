namespace jobPortal.Models.job
{
    public class AddJobModel
    {

      
        public string Title { get; set; }
        public string Description { get; set; }
        public string Resposibility { get; set; }
        public string Salary { get; set; }

        public string Category { get; set; }    
        public string AuthorId { get; set; }
    }
}
