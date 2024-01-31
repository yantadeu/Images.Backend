﻿using System.Text;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Images.Application.Common.Interfaces;
using Images.Domain.Models;
using Images.Infrastructure.Identity;

namespace Images.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new MediaTypeApiVersionReader("v");
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader()
            );
        });

        services.AddCors(opt =>
        {
            opt.AddPolicy(name: "CorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200", "http://localhost:5224", "https://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        AddAuthenticationAndAuthorization(services, configuration);

        services.AddAuthentication();

        services.AddHttpContextAccessor();

        services.AddScoped<IJwtUtils, JwtUtils>();
        services.AddScoped<SignInManager<ApplicationUser>>();

        return services;
    }

    private static void AddAuthenticationAndAuthorization(IServiceCollection services, IConfiguration configuration)
    {
        // Add Jwt Token Options
        var jwtSettings = Guard.Against.Null(configuration.GetSection("JwtOptions"));
        var googleAuthSettings = Guard.Against.Null(configuration.GetSection("GoogleAuthOptions"));

        var secret = Guard.Against.NullOrEmpty(jwtSettings.GetValue<string>("Secret"));
        var issuer = Guard.Against.NullOrEmpty(jwtSettings.GetValue<string>("Issuer"));
        var audience = Guard.Against.NullOrEmpty(jwtSettings.GetValue<string>("Audience"));

        var key = Encoding.ASCII.GetBytes(secret);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Default", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());

            options.AddPolicy("Administrator", new AuthorizationPolicyBuilder()
                .RequireRole("Administrator")
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());
        });
    }
}

