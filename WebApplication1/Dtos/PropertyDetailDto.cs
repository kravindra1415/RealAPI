namespace WebApplication1.Dtos
{
    public class PropertyDetailDto : PropertyListDto
    {
        public int CarpetArea { get; set; }
        public string Address { get; set; } = null!;
        public string Address2 { get; set; } = null!;
        public int FloorNo { get; set; }
        public int TotalFloors { get; set; }
        public string MainEntrance { get; set; } = null!;
        public int Security { get; set; }
        public bool Gated { get; set; }
        public int Maintenance { get; set; }
       
        public int Age { get; set; }
        public string Description { get; set; } = null!;
    }
}
