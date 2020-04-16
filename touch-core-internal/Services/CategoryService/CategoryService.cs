using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using touch_core_internal.Dtos.Category;
using touch_core_internal.Models;

namespace touch_core_internal.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        public CategoryService(IMapper mapper, DataContext dataContext)
        {
            this.Mapper = mapper;
            this.DataContext = dataContext;
        }

        public DataContext DataContext { get; private set; }

        public IMapper Mapper { get; private set; }

        public async Task<ServiceResponse<List<GetCategoryDto>>> AddNewCategoryAsync(AddCategoryDto newCategory)
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            var category = this.Mapper.Map<Category>(newCategory);
            await this.DataContext.Categories.AddAsync(category);
            await this.DataContext.SaveChangesAsync();

            serviceResponse.Data = await this.DataContext.Categories
                .Select(e => this.Mapper.Map<GetCategoryDto>(e))
                .ToListAsync();

            serviceResponse.UpdateResponseStatus($"Count of Categories: {serviceResponse.Data.Count}");
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategoryAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            try
            {
                var category = await this.DataContext.Categories
                    .FirstAsync(x => x.CategoryId == id);

                this.DataContext.Categories.Remove(category);
                await this.DataContext.SaveChangesAsync();

                serviceResponse.Data = await this.DataContext.Categories
                    .Select(x => this.Mapper.Map<GetCategoryDto>(x))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.UpdateResponseStatus("Category does not exist", false);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategoriesAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();

            var dbCategories = await DataContext.Categories
                .ToListAsync();

            serviceResponse.Data = dbCategories.Select(c => this.Mapper.Map<GetCategoryDto>(c)).ToList();
            serviceResponse.UpdateResponseStatus($"Count of Categories: {serviceResponse.Data.Count}");

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategoryByIdAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDto>();
            var dbCategory = await DataContext.Categories
                .FirstOrDefaultAsync(x => x.CategoryId == id);

            serviceResponse.Data = this.Mapper.Map<GetCategoryDto>(dbCategory);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategoryByNameAsync(string name)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDto>();
            var dbCategory = await DataContext.Categories
                .FirstOrDefaultAsync(x => x.Name.Equals(name));

            serviceResponse.Data = this.Mapper.Map<GetCategoryDto>(dbCategory);
            return serviceResponse;
        }
    }
}