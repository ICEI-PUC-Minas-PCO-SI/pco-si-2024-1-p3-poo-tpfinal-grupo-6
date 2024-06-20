﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BibCorpPrevenir2.api.Util.Services.Interfaces.Contracts.Uploads;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BibCorpPrevenir2.api.Util.Services.Interfaces.Implementations.Uploads
{
    public class UploadService : IUploadService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public UploadService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public void DeleteImageUpload(int contaId, string imageName, string destiny)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                var pathImage = Path.Combine(_hostEnvironment.ContentRootPath, @$"Resources/{destiny}", imageName);

                if (File.Exists(pathImage))
                {
                    File.Delete(pathImage);
                }
            };
        }

        public async Task<string> SaveImageUpload(int contaId, IFormFile imageNameUser, string destiny)
        {
            string imageNameNew = new String(Path
            .GetFileNameWithoutExtension(imageNameUser.FileName)
            .Take(15)
            .ToArray()
            ).Replace(' ', '-');

            imageNameNew = $"{imageNameNew}{DateTime.UtcNow:yymmssfff}{Path.GetExtension(imageNameUser.FileName)}";

            var pathImage = Path.Combine(_hostEnvironment.ContentRootPath, @$"Resources/{destiny}", imageNameNew);

            using (var fileStream = new FileStream(pathImage, FileMode.Create))
            {
                await imageNameUser.CopyToAsync(fileStream);
            };

            return imageNameNew;
        }
    }
}
