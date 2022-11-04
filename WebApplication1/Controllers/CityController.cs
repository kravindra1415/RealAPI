
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repository;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        //Get City
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await _cityRepository.GetCitiesAsync();
            return Ok(cities);
        }


        //Add City
        //api/city/add?cityname=Mumbai
        //[HttpPost("add")]
        //public async AddCity(City cityInput)  //using city object

        //[HttpPost("add/{cityName}")]
        //public async Task<IActionResult> AddCity(string cityName)
        //{
        //    City city = new()
        //    {
        //        Name = cityName
        //    };
        //    await _cityRepository.AddAsync(city);
        //    await _dbContext.SaveChangesAsync();

        //    return Ok(city);
        //}

        [HttpPost("post")]
        public async Task<IActionResult> AddCity(City cityName)
        {
            _cityRepository.AddCity(cityName);
            await _cityRepository.SaveAsync();
            return StatusCode(201);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            //var city = await _dbContext.Cities.FindAsync(id);
            //_dbContext.Remove(city);
            //await _dbContext.SaveChangesAsync();
            //return Ok(id);

            _cityRepository.DeleteCity(id);
            await _cityRepository.SaveAsync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public String GetById(int id)
        {
            return "abc";
        }
    }
}
