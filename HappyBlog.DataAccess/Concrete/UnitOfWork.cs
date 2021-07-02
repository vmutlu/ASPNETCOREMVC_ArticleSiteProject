using HappyBlog.DataAccess.Abstract;
using HappyBlog.DataAccess.Concrete.EntityFramework.Contexts;
using HappyBlog.DataAccess.Concrete.EntityFramework.Repositories;
using System.Threading.Tasks;

namespace HappyBlog.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HappyBlogContext _happyBlogContext;
        private EfArticleRepository _efArticleRepository;
        private EfCategoryRepository _efCategoryRepository;
        private EfCommentRepository _efCommentRepository;
        public UnitOfWork(HappyBlogContext happyBlogContext) => _happyBlogContext = happyBlogContext;
        public IArticleRepository Articles => _efArticleRepository ?? new EfArticleRepository(_happyBlogContext);

        public ICategoryRepository Categories => _efCategoryRepository ?? new EfCategoryRepository(_happyBlogContext);

        public ICommentRepository Comments => _efCommentRepository ?? new EfCommentRepository(_happyBlogContext);

        public async ValueTask DisposeAsync() => await _happyBlogContext.DisposeAsync().ConfigureAwait(false);

        public async Task<int> SaveAsync() => await _happyBlogContext.SaveChangesAsync().ConfigureAwait(false); // SaveChangesAsync() metodu int deger geriye döner
    }
}
