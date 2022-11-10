using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Repository.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Data.Repository
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly DataContext _dataContext;

        public PropertyTypeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<PropertyType>> GetPropertyTypeAsync()
        {
            return await _dataContext.PropertyTypes.ToListAsync();
        }
    }
}
