using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HappyBlog.Entities.DTOs
{
    public class UserUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string UserName { get; set; }

        [DisplayName("E-Mail Adresi")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(10, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(13, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(13, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Kullanıcı Resmi Ekle")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }

        [DisplayName("Kullanıcı Resmi")]
        public string Picture { get; set; }
    }
}
