using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Repository.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Data.Repository
{
    public class FurnishingTypeRepository : IFurnishingTypeRepository
    {
        private readonly DataContext _dataContext;

        public FurnishingTypeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync()
        {
            return await _dataContext.FurnishingTypes.ToListAsync();

        }
    }
}
