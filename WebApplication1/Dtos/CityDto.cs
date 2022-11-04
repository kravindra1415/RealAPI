using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is mandatory Field..")]
        [StringLength(25, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country is mandatory Field..")]

        public string Country { get; set; }
    }
}
