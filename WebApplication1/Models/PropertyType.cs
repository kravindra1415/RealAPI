using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PropertyType:BaseEntity
    {
        //public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
