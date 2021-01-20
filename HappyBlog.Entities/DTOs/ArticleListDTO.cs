using HappyBlog.Entities.Concrete;
using System.Collections.Generic;

namespace HappyBlog.Entities.DTOs
{
    public class ArticleListDTO: DTOGetBase
    {
        public IList<Article> Articles { get; set; }
    }
}
