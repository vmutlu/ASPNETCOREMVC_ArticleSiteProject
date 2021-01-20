using HappyBlog.DataAccess.Abstract;
using HappyBlog.Entities.Concrete;
using HappyBlog.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace HappyBlog.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfCommentRepository : EfEntityRepositoryBase<Comment>, ICommentRepository
    {
        public EfCommentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
