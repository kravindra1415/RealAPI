using WebApplication1.Models;

namespace WebApplication1.Data.Repository.Interfaces
{
    public interface IPropertyTypeRepository
    {
        Task<IEnumerable<PropertyType>> GetPropertyTypeAsync();

    }
}
