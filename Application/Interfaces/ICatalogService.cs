using Application.DTOs;

namespace Application.Interfaces;

public interface ICatalogService
{
    Task<List<CatalogGetDto>> GetAllAsync();
    Task<CatalogGetDto> GetByIdAsync(Guid id);
    Task<CatalogGetDto> CreateAsync(CatalogPostDto dto);
    Task<CatalogGetDto> UpdateAsync(Guid id, CatalogPutDto dto);
    Task<bool> DeleteAsync(Guid id);
}
