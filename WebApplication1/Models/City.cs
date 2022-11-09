using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class City: BaseEntity
    {
        //public int Id { get; set; }
        public string Name { get; set; } = null!;

        [Required]
        public string Country { get; set; } = null!;
        //public DateTime LastUpdatedOn { get; set; }
        //public int LastUpdatedBy { get; set; }
    }
}
