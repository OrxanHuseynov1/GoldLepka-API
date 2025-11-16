using Application.DTOs;

namespace Application.Interfaces;

public interface IProductService
{
    Task<List<ProductGetDto>> GetAllAsync();
    Task<ProductGetDto> GetByIdAsync(Guid id);
    Task<ProductGetDto> CreateAsync(ProductPostDto dto);
    Task<ProductGetDto> UpdateAsync(Guid id, ProductPutDto dto);
    Task<bool> DeleteAsync(Guid id);
}
