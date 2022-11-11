using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Data.Repository.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Data.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext _dataContext;

        public PropertyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddProperty(Property property)
        {
            _dataContext.Properties.Add(property);
        }

        public void DeleteProperty(int Id)
        {

        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int SellRent)
        {
            //var properties = await _dataContext.Properties.ToListAsync();
            var properties = await _dataContext.Properties
                .Include(p => p.PropertyType)
                .Include(p => p.City)
                .Include(p=>p.FurnishingType)
                .Where(p => p.SellRent == SellRent).ToListAsync();
            return properties;
        }

        public async Task<Property> GetPropertyDetailAsync(int Id)
        {
            var properties = await _dataContext.Properties
                .Include(p => p.PropertyType)
                .Include(p => p.City)
                .Include(p => p.FurnishingType)
                .Where(p => p.Id == Id).FirstAsync();

            return properties;
        }
    }
}
