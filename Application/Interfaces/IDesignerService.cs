using Application.DTOs;

namespace Application.Interfaces
{
    public interface IDesignerService
    {
        Task<IEnumerable<DesignerGetDto>> GetAllAsync();
        Task<DesignerGetDto> GetByIdAsync(Guid id);

        Task<DesignerGetDto> CreateAsync(DesignerPostDto dto, string uploadsFolderPath, string worksFolderPath);
        Task<DesignerGetDto> UpdateAsync(Guid id, DesignerPutDto dto, string uploadsFolderPath, string worksFolderPath);

        Task<bool> DeleteAsync(Guid id);
    }
}
