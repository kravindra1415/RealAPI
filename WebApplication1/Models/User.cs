using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User: BaseEntity
    {
        //public int Id { get; set; }
        [Required]
        public string? Username { get; set; } = null!;

        [Required]
        public byte[] Password { get; set; } = null!;
        public byte[] PasswordKey { get; set; } = null!;

    }
}
