using HappyBlog.Entities.DTOs;
using HappyBlog.Shared.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HappyBlog.Web.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadedDTO>> UploadedUserImage(string userName, IFormFile imageFile, string folderName = "userImages");
        IDataResult<ImageDeletedDTO> Delete(string pictureName);
    }
}
