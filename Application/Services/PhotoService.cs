

using Application.Handle.HandleImage;
using Application.IServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IConfiguration _configuration;
        private readonly IBaseRepository<Image> _baseImageRepository;
        private readonly CloudinarySettings _cloudinarySettings;
        private readonly Cloudinary _cloudinary;

        public PhotoService(IConfiguration configuration,
                            IBaseRepository<Image> baseImageRepository)
        {
            _configuration = configuration;
            _baseImageRepository = baseImageRepository;

            _cloudinarySettings = _configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            Account account = new Account
            {
                Cloud = _cloudinarySettings.CloudName,
                ApiKey = _cloudinarySettings.ApiKey,
                ApiSecret = _cloudinarySettings.ApiSecret
            };

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> AddPhotoAsync(IFormFile file)
        {
            try
            {
                var uploadResult = new ImageUploadResult();

                if (file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(file.FileName, stream),
                            Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                        };
                        uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    }
                }

                if (uploadResult.Error != null)
                {
                    return null;
                }

                return uploadResult.Url.ToString();
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
