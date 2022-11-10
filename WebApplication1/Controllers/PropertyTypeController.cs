using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repository.Interfaces;
using WebApplication1.Dtos;

namespace WebApplication1.Controllers
{

    public class PropertyTypeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PropertyTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //GET api/PropertyType/list
        [HttpGet("list")]
        [AllowAnonymous]

        public async Task<IActionResult> GetPropertyTypes()
        {
            var propertyTypes = await _unitOfWork.PropertyTypeRepository.GetPropertyTypeAsync();
            var propertyTypesDto = _mapper.Map<IEnumerable<KeyValuePairDto>>(propertyTypes);
            return Ok(propertyTypesDto);
        }
    }
}
