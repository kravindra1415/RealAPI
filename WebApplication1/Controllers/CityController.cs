
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

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

        [HttpGet]
        public IActionResult Get()
        {
           var cities=_dbContext.Cities.ToList();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public String GetById(int id)
        {
            return "abc";
        }
    }
}
