using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class CategoryHandler (AppDbContext appDbContext) : ICategoryHandler
{
    public async Task<Response<Category>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category();
            category.UserId = request.UserId;
            category.Title = request.Title;
            category.Description = request.Description;

            await appDbContext.Categories.AddAsync(category);
            await appDbContext.SaveChangesAsync();

            return new Response<Category>(category);
        }
        catch (Exception exception)
        {
            //study "Serilog"
            Console.WriteLine(exception.Message);
            throw new Exception("Failed to create category.");
        }
    }

    public async Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request)
    {
        return new Response<Category>();
    }

    public async Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
    {
        return new Response<Category>();
    }

    public async Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        return new Response<Category>();
    }

    public async Task<Response<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        return new Response<List<Category>>();
    }
}