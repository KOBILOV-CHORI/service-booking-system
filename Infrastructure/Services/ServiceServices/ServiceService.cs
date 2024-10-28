using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.ServiceServices;

public class ServiceService(DataContext context) : IServiceService
{
    public PaginationResponse<IEnumerable<ServiceReadDto>> GetAllServices(ServiceFilter filter)
    {
        IQueryable<Service> services = context.Services;
        if (!string.IsNullOrEmpty(filter.Name))
            services = services.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        if (!string.IsNullOrEmpty(filter.Description))
            services = services.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
        if (filter.MinPrice != null)
            services = services.Where(x => x.Price >= filter.MinPrice);
        if (filter.MaxPrice != null)
            services = services.Where(x => x.Price <= filter.MaxPrice);
        if (filter.OwnerId != null)
            services = services.Where(x => x.OwnerId == filter.OwnerId);
        if (filter.CategoryId != null)
            services = services.Where(x => x.CategoryId == filter.CategoryId);

        int totalRecords = services.Count();
        var result = services.Skip((filter.PageNumber - 1) * filter.PageSize)
                             .Take(filter.PageSize)
                             .Where(x => !x.IsDeleted)
                             .Select(x => x.ServiceToReadDto())
                             .ToList();

        return PaginationResponse<IEnumerable<ServiceReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public ServiceReadDto? GetServiceById(int id)
    {
        return context.Services.Where(x => x.IsDeleted == false && x.Id == id)
                               .Select(x => x.ServiceToReadDto())
                               .FirstOrDefault();
    }

    public bool CreateService(ServiceCreateDto createDto)
    {
        context.Services.Add(createDto.CreateDtoToService());
        context.SaveChanges();
        return true;
    }

    public bool UpdateService(ServiceUpdateDto updateDto)
    {
        var existingService = context.Services.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingService == null) return false;

        existingService.UpdateDtoToService(updateDto);
        context.SaveChanges();
        return true;
    }

    public bool DeleteService(int id)
    {
        var existingService = context.Services.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingService == null) return false;

        existingService.IsDeleted = true;
        existingService.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}
