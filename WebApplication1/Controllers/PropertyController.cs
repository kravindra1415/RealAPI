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

                var property = await _unitOfWork.PropertyRepository.GetPropertyDetailAsync(propId);

                var photo = new Photo
                {
                    imageURL = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId,
                };

                if (property.Photos.Count == 0)
                {
                    photo.IsPrimary = true;
                }

                property.Photos.Add(photo);
                await _unitOfWork.SaveAsync();
                return StatusCode(201);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //	https://res.cloudinary.com/demoupload/image/upload/v1668402440/yefb5j47c85dogv234b6.jpg
        //where photoPublicId is yefb5j47c85dogv234b6

        //Property/set-primary-photo/1/dasdsad
        [HttpPost("set-primary-photo/{propId}/{photoPublicId}")]
        [Authorize]
        public async Task<IActionResult> SetPrimaryPhoto(int propId, string photoPublicId)
        {
            var userId = GetUserId();
            var property = await _unitOfWork.PropertyRepository.GetPropertyByIdAsync(propId);

            if (property.PostedBy != userId)
                return BadRequest("you are not authorized to change the photo.");

            if (property == null)
                return BadRequest("no such property or photo exists..");

            var photo = property.Photos.FirstOrDefault(p => p.PublicId == photoPublicId);

            if (photo == null)
                return BadRequest("no such property or photo exists!!");

            if (photo.IsPrimary)
                return BadRequest("This is already a primary photo..");

            var currentPrimary = property.Photos.FirstOrDefault(p => p.IsPrimary);
            if (currentPrimary != null)
                currentPrimary.IsPrimary = false;
            photo.IsPrimary = true;

            if (await _unitOfWork.SaveAsync())
                return NoContent();

            return BadRequest("Some error has occured, failed to set primary photo..");
        }


        //Property/delete-photo/1/dasdsad
        [HttpDelete("delete-photo/{propId}/{photoPublicId}")]
        [Authorize]
        public async Task<IActionResult> DeletePhoto(int propId, string photoPublicId)
        {
            var userId = GetUserId();
            var property = await _unitOfWork.PropertyRepository.GetPropertyByIdAsync(propId);

            if (property.PostedBy != userId)
                return BadRequest("you are not authorized to delete the photo.");

            if (property == null)
                return BadRequest("no such property or photo exists..");

            var photo = property.Photos.FirstOrDefault(p => p.PublicId == photoPublicId);

            if (photo == null)
                return BadRequest("no such property or photo exists!!");

            if (photo.IsPrimary)
                return BadRequest("This is already a primary photo..");

            var result = await _photoService.DeletePhotoAsync(photoPublicId);
            if (result.Error != null)
                return BadRequest(result.Error.Message);

            //var currentPrimary = property.Photos.FirstOrDefault(p => p.IsPrimary);
            //if (currentPrimary != null)
            //    currentPrimary.IsPrimary = false;
            //photo.IsPrimary = true;

            property.Photos.Remove(photo);

            if (await _unitOfWork.SaveAsync())
                return Ok();

            return BadRequest("Some error has occured, failed to delete photo..");

        }
    }
}
