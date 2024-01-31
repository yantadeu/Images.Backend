﻿using Microsoft.AspNetCore.Http;
using Images.Application.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Images.Infrastructure.Identity;
internal class CurrentHttpRequest : ICurrentHttpRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentHttpRequest(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserId()
    {
        return _httpContextAccessor.HttpContext!.User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
    }
}
