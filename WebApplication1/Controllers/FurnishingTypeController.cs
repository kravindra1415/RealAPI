using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repository.Interfaces;
using WebApplication1.Dtos;

namespace WebApplication1.Controllers
{
    public class FurnishingTypeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FurnishingTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFurnishingTypes()
        {
            var furnishingTypes = await _unitOfWork.FurnishingTypeRepository.GetFurnishingTypesAsync();
            var furnishingTypeDto = _mapper.Map<IEnumerable<KeyValuePairDto>>(furnishingTypes);
            return Ok(furnishingTypeDto);
        }
    }
}
