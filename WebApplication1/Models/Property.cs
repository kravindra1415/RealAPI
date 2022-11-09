using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Property: BaseEntity
    {
        //public int Id { get; set; }
        public int SellRent { get; set; }
        public string Name { get; set; } = null!;
        public int PropertyTypeId { get; set; }

        public PropertyType PropertyType { get; set; } = null!;

        public int BHK { get; set; }
        public int FurnishingTypeId { get; set; }

        public FurnishingType FurnishingType { get; set; } = null!;

        public int Price { get; set; }
        public int BuiltArea { get; set; }
        public int CarpetArea { get; set; }
        public string Address { get; set; } = null!;
        public string Address2 { get; set; } = null!;
        public int CityId { get; set; }
        public City City { get; set; } = null!;

        public int FloorNo { get; set; }
        public int TotalFloors { get; set; }
        public bool ReadyToMove { get; set; }
        public string MainEntrance { get; set; } = null!;
        public int Security { get; set; }
        public bool Gated { get; set; }
        public int Maintenance { get; set; }
        public DateTime EstPossessionOn { get; set; }
        public int Age { get; set; }
        public string Description { get; set; } = null!;

        public ICollection<Photo> Photos { get; set; } = null!;

        public DateTime PostedOn { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int PostedBy { get; set; }
        public User User { get; set; } = null!;

    }
}
