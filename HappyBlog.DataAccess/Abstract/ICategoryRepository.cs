using HappyBlog.Entities.Concrete;
using HappyBlog.Shared.Data.Abstract;
using System.Threading.Tasks;

namespace HappyBlog.DataAccess.Abstract
{
    public interface ICategoryRepository : IEntityRepository<Category>
    {
        Task<Category> GetById(int categoryId);
    }
}
