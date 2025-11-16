using Application.DTOs;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserGetDto> LoginAsync(UserLoginDto dto);
    Task<List<UserGetDto>> GetAllAsync();
}
