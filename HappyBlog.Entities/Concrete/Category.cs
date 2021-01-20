using HappyBlog.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace HappyBlog.Entities.Concrete
{
    public class Category : EntityBase, IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
