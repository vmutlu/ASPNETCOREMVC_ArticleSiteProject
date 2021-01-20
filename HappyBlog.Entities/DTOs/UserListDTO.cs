using HappyBlog.Entities.Concrete;
using System.Collections.Generic;

namespace HappyBlog.Entities.DTOs
{
    public class UserListDTO : DTOGetBase
    {
        public IList<User> Users{ get; set; }
    }
}
