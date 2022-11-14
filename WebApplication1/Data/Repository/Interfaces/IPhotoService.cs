using CloudinaryDotNet.Actions;

namespace WebApplication1.Data.Repository.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo);
    }
}
