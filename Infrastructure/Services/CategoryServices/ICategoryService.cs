using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.CategoryServices;

public interface ICategoryService
{
    PaginationResponse<IEnumerable<CategoryReadDto>> GetAllCategories(CategoryFilter filter);    
    CategoryReadDto? GetCategoryById(int id);
    bool CreateCategory(CategoryCreateDto createDto);
    bool UpdateCategory(CategoryUpdateDto updateDto);
    bool DeleteCategory(int id);
}