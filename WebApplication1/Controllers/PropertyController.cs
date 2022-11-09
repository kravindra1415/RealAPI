using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repository.Interfaces;
using WebApplication1.Dtos;

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

        //Property/type/1
        [HttpGet("list/{sellRent}")]
        [AllowAnonymous]

        public async Task<IActionResult> GetPropertyList(int sellRent)
        {
            var property = await _unitOfWork.PropertyRepository.GetPropertiesAsync(sellRent);
            var propertyListDto = _mapper.Map<IEnumerable<PropertyListDto>>(property);

            return Ok(propertyListDto);
        }

        //Property
        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var property = await _unitOfWork.PropertyRepository.GetPropertyDetailAsync(id);
            var propertyDto = _mapper.Map<PropertyDetailDto>(property);

            return Ok(propertyDto);
        }

    }
}
