using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/categories")]
public sealed class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetCategories([FromQuery] CategoryFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<CategoryReadDto>>>.Success(null,
            categoryService.GetAllCategories(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetCategoryById(int id)
    {
        CategoryReadDto? res = categoryService.GetCategoryById(id);
        return res != null
            ? Ok(ApiResponse<CategoryReadDto?>.Success(null, res))
            : NotFound(ApiResponse<CategoryReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
    {
        CategoryCreateDto info = categoryCreateDto;
        bool res = categoryService.CreateCategory(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateCategory(CategoryUpdateDto info)
    {
        bool res = categoryService.UpdateCategory(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteCategory(int id)
    {
        bool res = categoryService.DeleteCategory(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}