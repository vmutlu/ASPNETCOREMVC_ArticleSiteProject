using HappyBlog.Entities.DTOs;
using HappyBlog.Shared.Utilities.Extensions;
using HappyBlog.Shared.Utilities.Results.Abstract;
using HappyBlog.Shared.Utilities.Results.ComplexTypes;
using HappyBlog.Shared.Utilities.Results.Concrete;
using HappyBlog.Web.Helpers.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HappyBlog.Web.Helpers.Concrate
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private readonly string _imgFolder = "img";
        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }

        public IDataResult<ImageDeletedDTO> Delete(string pictureName)
        {
            var fileToDelete = Path.Combine($"{_wwwroot}\\{_imgFolder}\\", pictureName);
            if (File.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete);

                var imageDeletedDTO = new ImageDeletedDTO
                {
                    FullName = pictureName,
                    Extension = fileInfo.Extension,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length
                };
                File.Delete(fileToDelete);
                return new DataResult<ImageDeletedDTO>(ResultStatus.Success, imageDeletedDTO);
            }

            else
                return new DataResult<ImageDeletedDTO>(ResultStatus.Error, "Böyle bir resim bulunamadı", null);
        }

        public async Task<IDataResult<ImageUploadedDTO>> UploadedUserImage(string userName, IFormFile pictureFile, string folderName = "userImages")
        {
            if (!Directory.Exists($"{_wwwroot}\\{_imgFolder}\\{folderName}"))
                Directory.CreateDirectory($"{_wwwroot}\\{_imgFolder}\\{folderName}");

            string fileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);
            string fileExtension = Path.GetExtension(pictureFile.FileName);

            DateTime dateTime = DateTime.Now;
            string fullFileName = $"{userName}_{dateTime.FullDateAndTimeStringWithUnderscore()}_{fileName}{fileExtension}";

            string path = Path.Combine($"{_wwwroot}\\{_imgFolder}\\{folderName}\\" + fullFileName);

            await using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }

            return new DataResult<ImageUploadedDTO>(ResultStatus.Success, $"{userName} adlı kullanıcısının resmi başarıyla yüklendi.", new ImageUploadedDTO
            {
                FullName = $"{folderName}\\{fullFileName}",
                OldName = fileName,
                Extension = fileExtension,
                FolderName = folderName,
                Path = path,
                Size = pictureFile.Length
            });
        }
    }
}
