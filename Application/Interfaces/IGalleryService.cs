using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IGalleryService
{
    Task<List<GalleryGetDto>> GetAllAsync();
    Task<GalleryGetDto> GetByIdAsync(Guid id);
    // Fayl artıq DTO daxilindən gəlir
    Task CreateAsync(GalleryPostDto dto);
    // Fayl artıq DTO daxilindən gəlir
    Task UpdateAsync(GalleryPutDto dto);
    Task DeleteAsync(Guid id);
}