using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Images.Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Images.Domain.Models;
using Images.Infrastructure.DbContexts;
using Images.Infrastructure.Identity;
using Images.Infrastructure.UserProfiles;

namespace Images.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        // Add Postgres DB Context
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));


        services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Default Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
            // Default Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false; // For special character
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
            // Default SignIn settings.
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.User.RequireUniqueEmail = true;
        });

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IApplicationUserService, ApplicationUserService>();
        services.AddScoped<ICurrentHttpRequest, CurrentHttpRequest>();
        
        return services;

    }
}

