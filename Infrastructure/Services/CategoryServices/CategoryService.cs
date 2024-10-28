using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.CategoryServices;

public class CategoryService(DataContext context) : ICategoryService
{
    public PaginationResponse<IEnumerable<CategoryReadDto>> GetAllCategories(CategoryFilter filter)
    {
        IQueryable<Category> categories = context.Categories;
        if (!string.IsNullOrEmpty(filter.Name))
            categories = categories.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

        int totalRecords = categories.Count();
        var result = categories.Skip((filter.PageNumber - 1) * filter.PageSize)
                               .Take(filter.PageSize)
                               .Where(x => !x.IsDeleted)
                               .Select(x => x.CategoryToReadDto())
                               .ToList();

        return PaginationResponse<IEnumerable<CategoryReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }
    public CategoryReadDto? GetCategoryById(int id)
    {
        return context.Categories.Where(x => x.IsDeleted == false && x.Id == id)
                                 .Select(x => x.CategoryToReadDto())
                                 .FirstOrDefault();
    }

    public bool CreateCategory(CategoryCreateDto createDto)
    {
        context.Categories.Add(createDto.CreateDtoToCategory());
        context.SaveChanges();
        return true;
    }

    public bool UpdateCategory(CategoryUpdateDto updateDto)
    {
        var existingCategory = context.Categories.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingCategory == null) return false;

        existingCategory.UpdateDtoToCategory(updateDto);
        context.SaveChanges();
        return true;
    }

    public bool DeleteCategory(int id)
    {
        var existingCategory = context.Categories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingCategory == null) return false;

        existingCategory.IsDeleted = true;
        existingCategory.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}
