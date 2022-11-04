using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Repository.Interfaces;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Data.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _dataContext;

        public CityRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void AddCity(City cityName)
        {
            var city = new City
            {
                Name = cityName.Name
            };

            _dataContext.Cities.Add(cityName);
        }

        public void DeleteCity(int id)
        {
            var city = _dataContext.Cities.Find(id);
            _dataContext.Cities.Remove(city);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _dataContext.Cities.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
