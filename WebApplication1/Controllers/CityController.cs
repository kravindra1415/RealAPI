
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public CityController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get City
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await _dbContext.Cities.ToListAsync();
            return Ok(cities);
        }


        //Add City
        //api/city/add?cityname=Mumbai
        //[HttpPost("add")]
        //public async Task<IActionResult> AddCity(City cityInput)  //using city object

        [HttpPost("add/{cityName}")]
        public async Task<IActionResult> AddCity(string cityName)
        {
            City city = new()
            {
                Name = cityName
            };
            await _dbContext.AddAsync(city);
            await _dbContext.SaveChangesAsync();

            return Ok(city);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _dbContext.Cities.FindAsync(id);
            _dbContext.Remove(city);
            await _dbContext.SaveChangesAsync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public String GetById(int id)
        {
            return "abc";
        }
    }
}
