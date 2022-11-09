using Microsoft.EntityFrameworkCore;
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

        }

        public void DeleteProperty(int Id)
        {

        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int SellRent)
        {
            var properties = await _dataContext.Properties.ToListAsync();
            return properties;
        }
    }
}
