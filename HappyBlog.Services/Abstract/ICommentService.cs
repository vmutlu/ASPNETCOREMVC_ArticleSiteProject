using HappyBlog.Shared.Utilities.Results.Abstract;
using System.Threading.Tasks;

namespace HappyBlog.Services.Abstract
{
    public interface ICommentService
    {
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
