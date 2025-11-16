using Application.DTOs;

namespace Application.Interfaces
{
    public interface IServiceService
    {
        Task<List<ServiceGetDto>> GetAllAsync();
        Task<ServiceGetDto> GetByIdAsync(Guid id);
        Task CreateAsync(ServicePostDto dto);
        Task UpdateAsync(ServicePutDto dto);
        Task DeleteAsync(Guid id);
    }
}
