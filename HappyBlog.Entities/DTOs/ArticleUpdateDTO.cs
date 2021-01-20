using HappyBlog.Entities.Concrete;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HappyBlog.Entities.DTOs
{
    public class ArticleUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Makale Başlığı")]
        [Required(ErrorMessage = "{0} alanı boş olmamalıdır.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string Title { get; set; }

        [DisplayName("Makale İçeriği")]
        [Required(ErrorMessage = "{0} alanı boş olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string Content { get; set; }

        [DisplayName("Thumbnail")]
        [Required(ErrorMessage = "{0} alanı boş olmamalıdır.")]
        [MaxLength(250, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string Thumbnail { get; set; }

        [DisplayName("Makale Tarihi")]
        [Required(ErrorMessage = "{0} alanı boş olmamalıdır.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Makale Yazarı")]
        [Required(ErrorMessage = "{0} alanı boş olmamalıdır.")]
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string SeoAuthor { get; set; }

        [DisplayName("Makale Seo Açıklaması")]
        [Required(ErrorMessage = "{0} alanı boş olmamalıdır.")]
        [MaxLength(150, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string SeoDescription { get; set; }

        [DisplayName("Makale Etiketleri")]
        [Required(ErrorMessage = "{0} alanı boş olmamalıdır.")]
        [MaxLength(70, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(1, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string SeoTags { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alanı boş olmamalıdır.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} alanı boş olmamalıdır.")]
        public bool IsActive { get; set; }

        [DisplayName("Silinsin Mi?")]
        [Required(ErrorMessage = "{0} alanı boş olmamalıdır.")]
        public bool IsDeleted { get; set; }
    }
}
