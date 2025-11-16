using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserGetDto> LoginAsync(UserLoginDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == dto.UserName && u.Password == dto.Password && !u.IsDeleted);

        return user == null ? null : _mapper.Map<UserGetDto>(user);
    }

    public async Task<List<UserGetDto>> GetAllAsync()
    {
        var users = await _context.Users.Where(u => !u.IsDeleted).ToListAsync();
        return _mapper.Map<List<UserGetDto>>(users);
    }
}
