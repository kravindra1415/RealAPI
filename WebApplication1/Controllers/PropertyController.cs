using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repository.Interfaces;

namespace WebApplication1.Controllers
{

    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PropertyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        //property/type/1
        [HttpGet("type/{sellRent}")]
        [AllowAnonymous]

        public async Task<IActionResult> GetPropertyList(int sellRent)
        {
            var property = await _unitOfWork.PropertyRepository.GetPropertiesAsync(sellRent);
            return Ok(property);
        }

    }
}
