using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HappyBlog.Entities.DTOs
{
    public class UserPasswordChangeDTO
    {
        [DisplayName("Mevcut Şifreniz")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(30, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("Yeni Şifreniz")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(30, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string NeWPassword { get; set; }

        [DisplayName("Yeni Şifreniz (Tekrar)")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(30, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        [Compare("NeWPassword",ErrorMessage ="Girmiş oldugunuz şifreler eşleşmiyor lütfen kontrol ederek tekrar giriniz !!!")]
        public string RepeatPassword { get; set; }
    }
}
