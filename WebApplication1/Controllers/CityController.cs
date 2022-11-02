
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "pune", "mumbai" };
        }

        [HttpGet("{id}")]
        public String GetById(int id)
        {
            return "abc";
        }

        public string Hello()
        {
            return "hello";
        }
    }
}
