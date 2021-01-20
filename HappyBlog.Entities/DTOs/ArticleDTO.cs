using HappyBlog.Entities.Concrete;
using HappyBlog.Shared.Utilities.Results.ComplexTypes;
using System;

namespace HappyBlog.Entities.DTOs
{
    public class ArticleDTO : DTOGetBase
    {
        public Article Article { get; set; }
    }
}
