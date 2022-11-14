using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Photos")]
    public class Photo : BaseEntity
    {
        [Required]
        public string PublicId { get; set; }
        [Required]
        public string imageURL { get; set; } = null!;
        public bool IsPrimary { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;
    }
}
