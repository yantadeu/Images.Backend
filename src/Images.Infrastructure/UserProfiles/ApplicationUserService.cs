﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Images.Application.Common.Exceptions;
using Images.Application.Common.Interfaces;
using Images.Application.Common.Models;
using Images.Domain.Models;
using Images.Infrastructure.Identity;

namespace Images.Infrastructure.UserProfiles;
internal class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<(string UserId, string FullName, string UserName, string Email, IList<string> Roles)> GetUserDetailsByUserNameAsync(string userName)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        var roles = await _userManager.GetRolesAsync(user);

        return (user.Id!, user.FullName!, user.UserName!, user.Email!, roles);
    }

    public async Task<(string UserId, string FullName, string UserName, string Email, IList<string> Roles)> GetUserDetailsByEmailAsync(string email)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        var roles = await _userManager.GetRolesAsync(user);

        return (user.Id!, user.FullName!, user.UserName!, user.Email!, roles);
    }

    public async Task<(string UserId, string FullName, string UserName, string Email, IList<string> Roles)> GetUserDetailsByUserIdAsync(string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        var roles = await _userManager.GetRolesAsync(user);

        return (user.Id!, user.FullName!, user.UserName!, user.Email!, roles);
    }

    
}
