using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using touch_core_internal.Dtos.Category;
using touch_core_internal.Services;
using touch_core_internal.Services.CategoryService;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        public CategoryController(ICategoryService categoryService)
        {
            this.CategoryService = categoryService;
        }

        public ICategoryService CategoryService { get; set; }

        //Do we really need this api?
        //If yes, then implement cascading. Rigtnow it is not implemented
        [Route("{id:guid}"), HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.CategoryService.DeleteCategoryAsync(id);

            if (serviceResponse.Data == null)
                return this.NotFound("Category not found");

            return this.Ok(serviceResponse);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var serviceResponse = await this.CategoryService.GetAllCategoriesAsync();
            return this.Ok(serviceResponse);
        }

        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetByIdAsync(Guid? id)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDto>();
            if (id.HasValue)
            {
                serviceResponse = await this.CategoryService.GetCategoryByIdAsync(id.Value);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.UpdateResponseStatus($"Category does not exist", false);
                    return this.NotFound(serviceResponse);
                }
                serviceResponse.UpdateResponseStatus("Category exist");
                return this.Ok(serviceResponse);
            }
            else
            {
                serviceResponse.UpdateResponseStatus($"Category does not exist", false);
                return this.NotFound("");
            }
        }

        [Route("upsert"), HttpPost]
        public virtual async Task<IActionResult> UpsertCategory(AddCategoryDto newCategory)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.CategoryService.AddNewCategoryAsync(newCategory);
            return this.Ok(serviceResponse);
        }
    }
}