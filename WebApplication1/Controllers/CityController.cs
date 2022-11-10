using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repository.Interfaces;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]

    [Authorize]
    public class CityController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //private readonly ICityRepository _cityRepository;

        public CityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Get City
        [HttpGet]
        [AllowAnonymous] // means except this method all methods are restricted
        //Specifies that the class or method that this attribute is applied to does not require authorization.

        public async Task<IActionResult> Get()
        {

            //var cities = await _cityRepository.GetCitiesAsync();
            var cities = await _unitOfWork.CityRepository.GetCitiesAsync();

            //var citiesDto = from c in cities
            //                select new CityDto()
            //                {
            //                    Id = c.Id,
            //                    Name = c.Name
            //                };

            var citiesDto = _mapper.Map<IEnumerable<CityDto>>(cities);
            //CityDto => destination
            //cities => source

            return Ok(citiesDto);


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
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            //_cityRepository.AddCity(cityName);
            //await _cityRepository.SaveAsync();

            var city = _mapper.Map<City>(cityDto);
            //City => Destination
            //citydto => Source
            city.LastUpdatedBy = 1;
            city.LastUpdatedOn = DateTime.Now;

            //var city = new City
            //{
            //    Name = cityDto.Name,
            //    LastUpdatedBy = 1,
            //    LastUpdatedOn = DateTime.Now,
            //};

            _unitOfWork.CityRepository.AddCity(city);
            await _unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        //[HttpPut("update/{id}")]
        //public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        //{
        //    var cityFromDb = await _unitOfWork.CityRepository.FindCity(id);
        //    cityFromDb.LastUpdatedOn = DateTime.Now;
        //    cityFromDb.LastUpdatedBy = 1;

        //    _mapper.Map(cityDto, cityFromDb); // source => cityDto   dest => cityFromDb
        //    await _unitOfWork.SaveAsync();

        //    return StatusCode(200);
        //}

        //[HttpPatch("update/{id}")]
        //public async Task<IActionResult> UpdateCityPatch(int id, JsonPatchDocument<City> cityToPatch)
        //{
        //    var cityFromDb = await _unitOfWork.CityRepository.FindCity(id);
        //    cityFromDb.LastUpdatedOn = DateTime.Now;
        //    cityFromDb.LastUpdatedBy = 1;

        //    //_mapper.Map(cityDto, cityFromDb); // source => cityDto   dest => cityFromDb
        //    cityToPatch.ApplyTo(cityFromDb, ModelState);

        //    await _unitOfWork.SaveAsync();
        //    return StatusCode(200);
        //}

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityUpdateDto cityDto)
        {
            try
            {

                var cityFromDb = await _unitOfWork.CityRepository.FindCity(id);
                cityFromDb.LastUpdatedOn = DateTime.Now;
                cityFromDb.LastUpdatedBy = 1;

                _mapper.Map(cityDto, cityFromDb); // source => cityDto   dest => cityFromDb

                throw new Exception("some unknown error occurred..");

                await _unitOfWork.SaveAsync();
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500, "some unknowon error occurred!!");
            }

        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            //var city = await _dbContext.Cities.FindAsync(id);
            //_dbContext.Remove(city);
            //await _dbContext.SaveChangesAsync();
            //return Ok(id);

            //_cityRepository.DeleteCity(id);
            //await _cityRepository.SaveAsync();

            _unitOfWork.CityRepository.DeleteCity(id);
            await _unitOfWork.SaveAsync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public String GetById(int id)
        {
            return "abc";
        }
    }
}
