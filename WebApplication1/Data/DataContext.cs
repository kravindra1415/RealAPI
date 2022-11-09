using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<FurnishingType> FurnishingTypes { get; set; } = null!;
        //public DbSet<Photo> Photos { get; set; } = null!;
        public DbSet<Property> Properties { get; set; } = null!;
        public DbSet<PropertyType> PropertyTypes { get; set; } = null!;



    }
}
