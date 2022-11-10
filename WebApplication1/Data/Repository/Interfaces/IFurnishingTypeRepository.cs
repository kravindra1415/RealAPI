using WebApplication1.Models;

namespace WebApplication1.Data.Repository.Interfaces
{
    public interface IFurnishingTypeRepository
    {
        Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync();
    }
}
