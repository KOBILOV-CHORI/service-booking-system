using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class ServiceMapperExtension
{
    public static ServiceReadDto ServiceToReadDto(this Service service)
    {
        return new ServiceReadDto()
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            Duration = service.Duration,
            Price = service.Price,
            OwnerId = service.OwnerId,
            CategoryId = service.CategoryId
        };
    }

    public static Service UpdateDtoToService(this Service service, ServiceUpdateDto updateDto)
    {
        service.Name = updateDto.Name;
        service.Description = updateDto.Description;
        service.Duration = updateDto.Duration;
        service.Price = updateDto.Price;
        service.OwnerId = updateDto.OwnerId;
        service.CategoryId = updateDto.CategoryId;
        service.Version += 1;
        service.UpdatedAt = DateTime.UtcNow;
        return service;
    }

    public static Service CreateDtoToService(this ServiceCreateDto createDto)
    {
        return new Service()
        {
            Name = createDto.Name,
            Description = createDto.Description,
            Duration = createDto.Duration,
            Price = createDto.Price,
            OwnerId = createDto.OwnerId,
            CategoryId = createDto.CategoryId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Service DeleteDtoToService(this Service service)
    {
        service.IsDeleted = true;
        service.DeletedAt = DateTime.UtcNow;
        service.Version += 1;
        service.UpdatedAt = DateTime.UtcNow;
        return service;
    }
}