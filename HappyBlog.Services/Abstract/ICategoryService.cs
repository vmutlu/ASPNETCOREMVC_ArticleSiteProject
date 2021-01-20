using HappyBlog.Entities.DTOs;
using HappyBlog.Shared.Utilities.Results.Abstract;
using System.Threading.Tasks;

namespace HappyBlog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDTO>> GetAsync(int categoryId);

        /// <summary>
        /// Verilen Id parametresine ait kategorinin CategoryUpdateDTO temsilini geri döner.
        /// </summary>
        /// <param name="categoryId"> 0'dan büyük int Id degeri</param>
        /// <returns>Asenkron bir operasyon ile Task olarak işlem sonucu DataResult tipinde geriye döner</returns>
        Task<IDataResult<CategoryUpdateDTO>> GetCategoryUpdateAsync(int categoryId);
        Task<IDataResult<CategoryListDTO>> GetAllAsync();

        /// <summary>
        /// Verilen CategoryAddDTO ve CreatedByName parametrelerine ait bilgiler ile yeni bir Category ekler.
        /// </summary>
        /// <param name="categoryAddDTO">categoryAddDTO tipinde eklenecek kategori bilgileri</param>
        /// <param name="createdByName">string tipinde kullanıcı Adı</param>
        /// <returns> Asenkron bir operasyon ile Task olarak işlemin sonucunu DataResult tipinde döner.</returns>
        Task<IDataResult<CategoryDTO>> AddAsync(CategoryAddDTO categoryAddDTO, string createdByName);
        Task<IDataResult<CategoryDTO>> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO, string modifiedByName);
        Task<IDataResult<CategoryDTO>> DeleteAsync(int categoryId, string modifiedByName);
        Task<IResult> HardDeleteAsync(int categoryId);
        Task<IDataResult<CategoryListDTO>> GetAllByNonDeletedAsync();
        Task<IDataResult<CategoryListDTO>> GetAllByNonDeletedAndActiveAsync();
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
