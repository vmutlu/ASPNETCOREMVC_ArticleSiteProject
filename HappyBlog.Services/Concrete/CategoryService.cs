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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CategoryDTO>> AddAsync(CategoryAddDTO categoryAddDTO, string createdByName)
        {
            var category = _mapper.Map<Category>(categoryAddDTO);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;

            var addedCategory = await _unitOfWork.Categories.AddAsync(category);

            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDTO>(ResultStatus.Success, Messages.Category.Add(addedCategory.Name), new CategoryDTO
            {
                Category = addedCategory,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Category.Add(categoryAddDTO.Name)
            });
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var categories = await _unitOfWork.Categories.CountAsync();
            if (categories > -1)
                return new DataResult<int>(ResultStatus.Success, categories);
            else
                return new DataResult<int>(ResultStatus.Error, "Beklenmedik bir hata oluştu.", -1);
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var categories = await _unitOfWork.Categories.CountAsync(c => !c.IsDeleted);
            if (categories > -1)
                return new DataResult<int>(ResultStatus.Success, categories);
            else
                return new DataResult<int>(ResultStatus.Error, "Beklenmedik bir hata oluştu.", -1);
        }

        public async Task<IDataResult<CategoryDTO>> DeleteAsync(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;

                var deletedCategory = await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDTO>(ResultStatus.Success, Messages.Category.Delete(category.Name), new CategoryDTO
                {
                    Category = deletedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Category.Delete(category.Name)
                });
            }

            else
                return new DataResult<CategoryDTO>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", new CategoryDTO
                {
                    Category = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Category.NotFound(isPlural: false)
                });
        }

        public async Task<IDataResult<CategoryDTO>> GetAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);

            if (category != null)
                return new DataResult<CategoryDTO>(ResultStatus.Success, new CategoryDTO
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                });

            else
                return new DataResult<CategoryDTO>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), new CategoryDTO
                {
                    Category = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Category.NotFound(isPlural: false)
                });
        }

        public async Task<IDataResult<CategoryListDTO>> GetAllAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null);

            if (categories.Count > -1)
                return new DataResult<CategoryListDTO>(ResultStatus.Success, new CategoryListDTO
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });

            else
                return new DataResult<CategoryListDTO>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), new CategoryListDTO
                {
                    Categories = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Category.NotFound(isPlural: true)
                });
        }

        public async Task<IDataResult<CategoryListDTO>> GetAllByNonDeletedAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted);

            if (categories.Count > -1)
                return new DataResult<CategoryListDTO>(ResultStatus.Success, new CategoryListDTO
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });

            else
                return new DataResult<CategoryListDTO>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), new CategoryListDTO
                {
                    Categories = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Category.NotFound(isPlural: true)
                });
        }

        public async Task<IDataResult<CategoryListDTO>> GetAllByNonDeletedAndActiveAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive);

            if (categories.Count > -1)
                return new DataResult<CategoryListDTO>(ResultStatus.Success, new CategoryListDTO
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });

            else
                return new DataResult<CategoryListDTO>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<CategoryUpdateDTO>> GetCategoryUpdateAsync(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdate = _mapper.Map<CategoryUpdateDTO>(category);
                return new DataResult<CategoryUpdateDTO>(ResultStatus.Success, categoryUpdate);
            }

            else
                return new DataResult<CategoryUpdateDTO>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), null);
        }

        public async Task<IResult> HardDeleteAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (category != null)
            {
                category.IsDeleted = true;

                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Category.HardDelete(category.Name));
            }

            else
                return new Result(ResultStatus.Error, Messages.Category.HardDelete(category.Name));
        }

        public async Task<IDataResult<CategoryDTO>> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO, string modifiedByName)
        {
            var oldCategory = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDTO.Id);
            var category = _mapper.Map<CategoryUpdateDTO, Category>(categoryUpdateDTO, oldCategory);
            category.ModifiedByName = modifiedByName;

            var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDTO>(ResultStatus.Success, Messages.Category.Update(categoryUpdateDTO.Name), new CategoryDTO
            {
                Category = updatedCategory,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Category.Update(categoryUpdateDTO.Name)
            });
        }
    }
}
