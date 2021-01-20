using HappyBlog.Entities.Concrete;
using HappyBlog.Shared.Data.Abstract;

namespace HappyBlog.DataAccess.Abstract
{
    public interface ICommentRepository: IEntityRepository<Comment>
    {
    }
}
