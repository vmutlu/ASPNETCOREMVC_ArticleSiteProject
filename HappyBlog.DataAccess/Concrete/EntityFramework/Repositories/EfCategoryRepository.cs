using HappyBlog.DataAccess.Abstract;
using HappyBlog.DataAccess.Concrete.EntityFramework.Contexts;
using HappyBlog.Entities.Concrete;
using HappyBlog.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HappyBlog.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> GetById(int categoryId)
        {
           return await happyBlogContext.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
        } 

        private HappyBlogContext happyBlogContext
        {
            get
            {
                return _dbContext as HappyBlogContext;
            }
        }
    }
}
