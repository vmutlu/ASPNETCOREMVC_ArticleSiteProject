using HappyBlog.Entities.Concrete;
using System.Collections.Generic;

namespace HappyBlog.Entities.DTOs
{
    public class CategoryListDTO : DTOGetBase
    {
       public IList<Category> Categories { get; set; }
    }
}
