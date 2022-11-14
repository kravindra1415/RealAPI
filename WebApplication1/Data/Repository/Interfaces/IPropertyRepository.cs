using WebApplication1.Models;

namespace WebApplication1.Data.Repository.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetPropertiesAsync(int SellRent);
        void AddProperty(Property property);
        void DeleteProperty(int Id);
        Task<Property> GetPropertyDetailAsync(int Id);
        Task<Property> GetPropertyByIdAsync(int Id);
    }
}
