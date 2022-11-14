using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repository.Interfaces;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public PropertyController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoService = photoService;
        }

        //Property/list/1
        [HttpGet("list/{sellRent}")]
        [AllowAnonymous]

        public async Task<IActionResult> GetPropertyList(int sellRent)
        {
            var property = await _unitOfWork.PropertyRepository.GetPropertiesAsync(sellRent);
            var propertyListDto = _mapper.Map<IEnumerable<PropertyListDto>>(property);

            return Ok(propertyListDto);
        }

        //Property/detail/1
        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var property = await _unitOfWork.PropertyRepository.GetPropertyDetailAsync(id);
            var propertyDto = _mapper.Map<PropertyDetailDto>(property);

            return Ok(propertyDto);
        }

        //Property/add/1
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddProperty(PropertyDto propertyDto)
        {
            try
            {
                var property = _mapper.Map<Property>(propertyDto);
                //property.PostedBy = 1;
                //property.LastUpdatedBy = 1;

                var userId = GetUserId();
                property.LastUpdatedBy = userId;
                property.PostedBy = userId;

                _unitOfWork.PropertyRepository.AddProperty(property);
                await _unitOfWork.SaveAsync();

                return StatusCode(201);
            }
            catch (Exception)
            {

                throw;
            }

        }

        //Property/add/photo/1
        [HttpPost("add/photo/{propId}")]
        [Authorize]
        public async Task<IActionResult> AddPropertyPhoto(IFormFile file, int propId)
        {
            try
            {
                var result = await _photoService.UploadPhotoAsync(file);
                if (result.Error != null)
                {
                    return BadRequest(result.Error.Message);
                }
                return Ok(201);
            }
            catch (Exception)   
            {

                throw;
            }
        }
    }
}
