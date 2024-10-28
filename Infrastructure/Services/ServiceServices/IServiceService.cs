using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.ServiceServices;

public interface IServiceService
{
    PaginationResponse<IEnumerable<ServiceReadDto>> GetAllServices(ServiceFilter filter);
    ServiceReadDto? GetServiceById(int id);
    bool CreateService(ServiceCreateDto createDto);
    bool UpdateService(ServiceUpdateDto updateDto);
    bool DeleteService(int id);
}