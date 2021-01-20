using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HappyBlog.Entities.DTOs
{
    public class CategoryAddDTO
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} nı Boş Geçemezsiniz.")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden büyük olamaz.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olamaz.")]
        public string Name { get; set; }

        [DisplayName("Kategori Açıklaması")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden büyük olamaz.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olamaz.")]
        public string Description { get; set; }

        [DisplayName("Kategori Özel Not")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden büyük olamaz.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olamaz.")]
        public string Note { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} nı Boş Geçemezsiniz.")]
        public bool IsActive { get; set; }
    }
}
