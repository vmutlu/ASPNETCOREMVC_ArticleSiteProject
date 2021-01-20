using AutoMapper;
using HappyBlog.DataAccess.Abstract;
using HappyBlog.Entities.Concrete;
using HappyBlog.Entities.DTOs;
using HappyBlog.Services.Abstract;
using HappyBlog.Services.Utilities;
using HappyBlog.Shared.Utilities.Results.Abstract;
using HappyBlog.Shared.Utilities.Results.ComplexTypes;
using HappyBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Threading.Tasks;

namespace HappyBlog.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<ArticleDTO>> AddAsync(ArticleAddDTO articleAddDTO, string createdByName)
        {
            var article = _mapper.Map<Article>(articleAddDTO);

            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            article.UserId = 1;

            var addedArticle = await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.SaveAsync();
            return new DataResult<ArticleDTO>(ResultStatus.Success, Messages.Article.Add(addedArticle.Title), new ArticleDTO
            {
                Article = addedArticle,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Article.Add(addedArticle.Title)
            });
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var articles = await _unitOfWork.Articles.CountAsync();

            if (articles > -1)
                return new DataResult<int>(ResultStatus.Success, articles);
            else
                return new DataResult<int>(ResultStatus.Error, "Beklenmedik hata oluştu", -1);
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var articles = await _unitOfWork.Articles.CountAsync(a => !a.IsDeleted);

            if (articles > -1)
                return new DataResult<int>(ResultStatus.Success, articles);
            else
                return new DataResult<int>(ResultStatus.Error, "Beklenmedik hata oluştu", -1);
        }

        public async Task<IResult> DeleteAsync(int articleId, string modifiedByName)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);

            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;

                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Article.Delete(article.Title));
            }

            else
                return new Result(ResultStatus.Error, $"{articleId} id'sine sahip makale bulunamadı.");
        }

        public async Task<IDataResult<ArticleDTO>> GetAsync(int articleId)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId, a => a.User, a => a.Category);

            if (article != null)
                return new DataResult<ArticleDTO>(ResultStatus.Success, new ArticleDTO
                {
                    Article = article,
                    ResultStatus = ResultStatus.Success
                });

            else
                return new DataResult<ArticleDTO>(ResultStatus.Error, Messages.Article.NotFound(isPlural: false), null);
        }

        public async Task<IDataResult<ArticleListDTO>> GetAllAsync()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(null, a => a.User, a => a.Category);

            if (articles.Count > -1)
                return new DataResult<ArticleListDTO>(ResultStatus.Success, new ArticleListDTO
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });

            else
                return new DataResult<ArticleListDTO>(ResultStatus.Error, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<ArticleListDTO>> GetAllByCategoryAsync(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted && a.IsActive, u => u.User, c => c.Category);

                if (articles.Count > -1)
                    return new DataResult<ArticleListDTO>(ResultStatus.Success, new ArticleListDTO
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    });

                else
                    return new DataResult<ArticleListDTO>(ResultStatus.Error, Messages.Article.NotFound(isPlural: true), null);
            }

            else
                return new DataResult<ArticleListDTO>(ResultStatus.Error, Messages.Article.NotFound(isPlural: false), null);
        }

        public async Task<IDataResult<ArticleListDTO>> GetAllByNonDeletedAsync()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted, u => u.User, c => c.Category);

            if (articles.Count > -1)
                return new DataResult<ArticleListDTO>(ResultStatus.Success, new ArticleListDTO
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });

            else
                return new DataResult<ArticleListDTO>(ResultStatus.Error, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<ArticleListDTO>> GetAllByNonDeletedAndActiveAsync()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive, u => u.User, c => c.Category);

            if (articles.Count > -1)
                return new DataResult<ArticleListDTO>(ResultStatus.Success, new ArticleListDTO
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });

            else
                return new DataResult<ArticleListDTO>(ResultStatus.Error, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IResult> HardDeleteAsync(int articleId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);

            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);

                await _unitOfWork.Articles.DeleteAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Article.HardDelete(article.Title));
            }

            else
                return new Result(ResultStatus.Error, "Makale veri tabanından silinemedi !");
        }

        public async Task<IDataResult<ArticleDTO>> UpdateAsync(ArticleUpdateDTO articleUpdateDTO, string modifiedByName)
        {
            var article = _mapper.Map<Article>(articleUpdateDTO);
            article.ModifiedByName = modifiedByName;

            var updatedArticle = await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();
            return new DataResult<ArticleDTO>(ResultStatus.Success, Messages.Article.Update(updatedArticle.Title), new ArticleDTO
            {
                Article = updatedArticle,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Article.Update(updatedArticle.Title)
            });
        }
    }
}
