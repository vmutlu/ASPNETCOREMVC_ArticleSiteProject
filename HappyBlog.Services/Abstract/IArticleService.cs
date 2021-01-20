using HappyBlog.Entities.DTOs;
using HappyBlog.Shared.Utilities.Results.Abstract;
using System.Threading.Tasks;

namespace HappyBlog.Services.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult<ArticleDTO>> GetAsync(int articleId);
        Task<IDataResult<ArticleListDTO>> GetAllAsync();
        Task<IDataResult<ArticleDTO>> AddAsync(ArticleAddDTO articleAddDTO, string createdByName);
        Task<IDataResult<ArticleDTO>> UpdateAsync(ArticleUpdateDTO articleUpdateDTO, string modifiedByName);
        Task<IResult> DeleteAsync(int articleId, string modifiedByName);
        Task<IResult> HardDeleteAsync(int articleId);
        Task<IDataResult<ArticleListDTO>> GetAllByNonDeletedAsync();
        Task<IDataResult<ArticleListDTO>> GetAllByNonDeletedAndActiveAsync();
        Task<IDataResult<ArticleListDTO>> GetAllByCategoryAsync(int categoryId);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
