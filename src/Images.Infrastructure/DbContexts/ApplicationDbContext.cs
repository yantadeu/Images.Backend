using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Images.Application.Common.Interfaces;
using Images.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Images.Infrastructure.DbContexts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public ApplicationDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    public DbSet<Image> Images { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    private IConfiguration _configuration;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var userId = Guid.NewGuid();
        var owner = new ApplicationUser
        {
            Id = userId.ToString(),
            FullName = "Admin",
            UserName = "admin",
            NormalizedEmail = "ADMIN@LOCALHOST",
            NormalizedUserName = "ADMIN",
            SecurityStamp = "NRHT2F77S7FHBSUUKFIKIBYIDORERBGK",
            ConcurrencyStamp = "04cb35b1-ac8d-4522-8991-4215b65aa595",
            TwoFactorEnabled = false,
            Email = "admin@localhost",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };
        var pass = new PasswordHasher<object>().HashPassword(owner, "P@ssw0rd@123");
        owner.PasswordHash = pass;
        
        modelBuilder.Entity<ApplicationUser>().HasData(
            owner
        );

        var roleId = Guid.NewGuid();
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = roleId.ToString(),
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            }
        );

        
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = roleId.ToString(),
                UserId = userId.ToString()
            }
        );
        

        modelBuilder.Entity<Image>().HasData(
            new Image
            {
                Id = Guid.NewGuid(), Title = "Image 1",
                Content = GetImageAsByteArrayAsync("https://placehold.co/100/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.NewGuid(), Title = "Image 2",
                Content = GetImageAsByteArrayAsync("https://placehold.co/200/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.NewGuid(), Title = "Image 3",
                Content = GetImageAsByteArrayAsync("https://placehold.co/300/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.NewGuid(), Title = "Image 4",
                Content = GetImageAsByteArrayAsync("https://placehold.co/400/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.NewGuid(), Title = "Image 5",
                Content = GetImageAsByteArrayAsync("https://placehold.co/500/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.NewGuid(), Title = "Image 6",
                Content = GetImageAsByteArrayAsync("https://placehold.co/600/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.NewGuid(), Title = "Image 7",
                Content = GetImageAsByteArrayAsync("https://placehold.co/700/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.NewGuid(), Title = "Image 8",
                Content = GetImageAsByteArrayAsync("https://placehold.co/800/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.NewGuid(), Title = "Image 9",
                Content = GetImageAsByteArrayAsync("https://placehold.co/900/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.NewGuid(), Title = "Image 10",
                Content = GetImageAsByteArrayAsync("https://placehold.co/1000/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            }
        );


    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection") ??
                               "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=Upik@123";

        optionsBuilder.UseNpgsql(connectionString);
    }
    

    private async Task<string> GetImageAsByteArrayAsync(string imageUrl)
    {
        var httpClientHandler = new HttpClientHandler();
        httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
        var httpClient = new HttpClient(httpClientHandler);
        using var response = await httpClient.GetAsync(imageUrl);
        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException("Could not retrieve image from the URL.");
        var bytes = await response.Content.ReadAsByteArrayAsync();
        using var stream = new MemoryStream();
        return Convert.ToBase64String(bytes);
    }
}

