using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class CategoryMapperExtension
{
    public static CategoryReadDto CategoryToReadDto(this Category category)
    {
        return new CategoryReadDto()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }

    public static Category UpdateDtoToCategory(this Category category, CategoryUpdateDto updateDto)
    {
        category.Name = updateDto.Name;
        category.Description = updateDto.Description;
        category.Version += 1;
        category.UpdatedAt = DateTime.UtcNow;
        return category;
    }

    public static Category CreateDtoToCategory(this CategoryCreateDto createDto)
    {
        return new Category()
        {
            Name = createDto.Name,
            Description = createDto.Description,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Category DeleteDtoToCategory(this Category category)
    {
        category.IsDeleted = true;
        category.DeletedAt = DateTime.UtcNow;
        category.Version += 1;
        category.UpdatedAt = DateTime.UtcNow;
        return category;
    }
}