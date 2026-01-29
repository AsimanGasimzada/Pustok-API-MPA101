using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.Exceptions;
using Pustok.Business.Services.Abstractions;
using Pustok.Core.Entities;
using Pustok.Core.Enums;

namespace Pustok.Business.Services.Implementations;

internal class AuthService(UserManager<AppUser> _userManager, IMapper _mapper) : IAuthService
{
    public async Task<ResultDto> RegisterAsync(RegisterDto dto)
    {
        var isExistEmail = await _userManager.Users.AnyAsync(x => x.Email!.ToLower() == dto.Email.ToLower());


        //if (isExistEmail)
        //    throw new AlreadyExistException("This email is already exist");

        //var isExistUserName = await _userManager.Users.AnyAsync(x => x.UserName!.ToLower() == dto.UserName.ToLower());

        //if (isExistUserName)
        //    throw new AlreadyExistException("This username is already exist");

        var appUser = _mapper.Map<AppUser>(dto);

        var result = await _userManager.CreateAsync(appUser, dto.Password);

        if (!result.Succeeded)
        {
            string message = string.Join(",\n", result.Errors.Select(x => x.Description));

            throw new RegisterException(message);
        }


        await _userManager.AddToRoleAsync(appUser, IdentityRoles.Member.ToString());


        return new();

    }
}
