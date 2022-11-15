﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using WebApplication1.Data.Repository.Interfaces;

namespace WebApplication1.Service
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;
        public PhotoService(IConfiguration configuration)
        {
            //Account account = new Account("my_cloud_name", "my_api_key", "my_api_secret");
            Account account = new(configuration.GetSection("CloudinarySettings:CloudName").Value,
                configuration.GetSection("CloudinarySettings:ApiKey").Value,
                configuration.GetSection("CloudinarySettings:ApiSecret").Value);

            cloudinary = new Cloudinary(account);

        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await cloudinary.DestroyAsync(deleteParams);

            return result;

        }

        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo)
        {
            var uploadResult = new ImageUploadResult();
            if (photo.Length > 0)
            {
                using var stream = photo.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(photo.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(800),

                };

                uploadResult = await cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }
    }
}
