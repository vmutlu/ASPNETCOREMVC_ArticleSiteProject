using System;
using System.Threading.Tasks;

namespace HappyBlog.DataAccess.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IArticleRepository Articles { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }

        Task<int> SaveAsync(); // etkilenen kayıt sayısını almak istediğimde int lazım olcak.
    }
}
