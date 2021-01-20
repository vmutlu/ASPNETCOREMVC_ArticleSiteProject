using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HappyBlog.Entities.DTOs
{
    public class UserAddDTO
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage ="{0} boş geçilemez.")]
        [MaxLength(50,ErrorMessage ="{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(3,ErrorMessage ="{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string UserName { get; set; }

        [DisplayName("E-Mail Adresi")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(10, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(30, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(13, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(13, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Kullanıcı Resmi")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        public string Picture { get; set; }
    }
}
