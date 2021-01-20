using HappyBlog.Entities.Concrete;

namespace HappyBlog.Entities.DTOs
{
    public class UserDTO : DTOGetBase
    {
        public User User { get; set; }
    }
}
