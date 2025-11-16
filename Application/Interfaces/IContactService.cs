using Application.DTOs;

namespace Application.Interfaces;

public interface IContactService
{
    Task<List<ContactGetDto>> GetAllAsync();
    Task<ContactGetDto> GetByIdAsync(Guid id);
    Task<ContactGetDto> CreateAsync(ContactPostDto dto);
    Task<ContactGetDto> UpdateAsync(Guid id, ContactPutDto dto);
    Task<bool> DeleteAsync(Guid id);
}
