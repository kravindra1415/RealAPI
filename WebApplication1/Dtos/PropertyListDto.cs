namespace WebApplication1.Dtos
{
    public class PropertyListDto
    {
        public int Id { get; set; }
        public int SellRent { get; set; }
        public string Name { get; set; } = null!;
        public string PropertyType { get; set; } = null!;
        public string FurnishingType { get; set; } = null!;
        public int Price { get; set; }
        public int BHK { get; set; }
        public int BuiltArea { get; set; }
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public bool ReadyToMove { get; set; }
        public DateTime EstPosessionOn { get; set; }
    }
}
