using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using touch_core_internal.Dtos.Category;

namespace touch_core_internal.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<GetCategoryDto>>> AddNewCategoryAsync(AddCategoryDto newTimeSheet);

        Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategoryAsync(Guid id);

        Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategoriesAsync();

        Task<ServiceResponse<GetCategoryDto>> GetCategoryByIdAsync(Guid id);
    }
}