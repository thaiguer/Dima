using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Dima.Api.Handlers;

public class CategoryHandler (AppDbContext appDbContext) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category();
            category.UserId = request.UserId;
            category.Title = request.Title;
            category.Description = request.Description;

            await appDbContext.Categories.AddAsync(category);
            await appDbContext.SaveChangesAsync();

            return new Response<Category?>(category, 201, "Category created with success");
        }
        catch
        {
            //study "Serilog"
            return new Response<Category?>(null, 500, "Impossible to create the category");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if (category == null) return new Response<Category?>(null, 404, "Category not found");

            category.Title = request.Title;
            category.Description = request.Description;

            appDbContext.Categories.Update(category);
            await appDbContext.SaveChangesAsync();
            return new Response<Category?>(category, message: "Category successfully updated");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Impossible to update the category");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if(category == null) return new Response<Category?>(null, code: 404, message: "Category not found");
            
            appDbContext.Categories.Remove(category);
            await appDbContext.SaveChangesAsync();
            return new Response<Category?>(category, message: "Category deleted with success");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Impossible to delete the category");
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await appDbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if (category == null) return new Response<Category?>(null, code: 404, message: "Category not found");

            var response = new Response<Category?>();
            response.Message = "Category in the database";
            response.Data = category;
            return response;
        }
        catch
        {
            return new Response<Category?>(null, 500, "Impossible to get category");
        }
    }

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = appDbContext
                .Categories
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Title);

            var categories = await query
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            var response = new PagedResponse<List<Category>>(categories, count, request.PageNumber, request.PageSize);
            response.Message = count >= 1 ? $"There are {count} categories in the database" : "There are no categories in the database";
            return response;
        }
        catch
        {
            return new PagedResponse<List<Category>>(null, 500, "Impossible to get categories");
        }
    }
}