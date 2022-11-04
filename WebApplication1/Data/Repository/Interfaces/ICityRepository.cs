using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Data.Repository.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        void AddCity(City cityDto);
        void DeleteCity(int id);
        Task<bool> SaveAsync();

        Task<City> FindCity(int id);

    }
}
 