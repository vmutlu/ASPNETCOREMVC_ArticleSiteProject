using HappyBlog.DataAccess.Abstract;
using HappyBlog.Services.Abstract;
using HappyBlog.Shared.Utilities.Results.Abstract;
using HappyBlog.Shared.Utilities.Results.ComplexTypes;
using HappyBlog.Shared.Utilities.Results.Concrete;
using System.Threading.Tasks;

namespace HappyBlog.Services.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var comments = await _unitOfWork.Comments.CountAsync();

            if (comments > -1)
                return new DataResult<int>(ResultStatus.Success, comments);
            else
                return new DataResult<int>(ResultStatus.Error, "Beklenmedik bir hata oluştu", -1);
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var comments = await _unitOfWork.Comments.CountAsync(c => !c.IsDeleted);

            if (comments > -1)
                return new DataResult<int>(ResultStatus.Success, comments);
            else
                return new DataResult<int>(ResultStatus.Error, "Beklenmedik bir hata oluştu", -1);
        }
    }
}
