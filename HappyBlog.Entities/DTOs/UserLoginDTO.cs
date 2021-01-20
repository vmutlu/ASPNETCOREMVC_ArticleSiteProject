using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HappyBlog.Entities.DTOs
{
    public class UserLoginDTO
    {

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

        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
