using HappyBlog.DataAccess.Abstract;
using HappyBlog.Entities.Concrete;
using HappyBlog.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace HappyBlog.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfArticleRepository : EfEntityRepositoryBase<Article>, IArticleRepository
    {
        public EfArticleRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
