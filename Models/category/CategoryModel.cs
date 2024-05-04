using jobPortal.Models.job;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace jobPortal.Models.category
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public IEnumerable<JobModel> jobModels { get; set; }
    }
}
