using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Images.Application.Common.Interfaces;
using Images.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Images.Infrastructure.DbContexts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext()
    {
    }
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
                Id = Guid.Parse("1b7b6d51-4006-447d-b5b6-fffd4f800e55"), Title = "Image 1",
                Content = GetImageAsByteArrayAsync("https://placehold.co/100/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.Parse("2cbf533f-0055-42ea-b788-ff8207ce7a16"), Title = "Image 2",
                Content = GetImageAsByteArrayAsync("https://placehold.co/200/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.Parse("42368e89-cc74-4766-86a3-abb4fb522aee"), Title = "Image 3",
                Content = GetImageAsByteArrayAsync("https://placehold.co/300/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.Parse("6325c87b-45cb-4571-9ebc-26a0a64c85be"), Title = "Image 4",
                Content = GetImageAsByteArrayAsync("https://placehold.co/400/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.Parse("68ac7138-b5a7-4e5e-a4ee-e2210d60da6b"), Title = "Image 5",
                Content = GetImageAsByteArrayAsync("https://placehold.co/500/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.Parse("746ca9da-92cd-44c6-9f35-964645173a18"), Title = "Image 6",
                Content = GetImageAsByteArrayAsync("https://placehold.co/600/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.Parse("90ec12f1-7779-415e-9e7b-5f70a3db10a4"), Title = "Image 7",
                Content = GetImageAsByteArrayAsync("https://placehold.co/700/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.Parse("96d4bdd2-a0c8-4998-a14a-3d3ebf1f5c66"), Title = "Image 8",
                Content = GetImageAsByteArrayAsync("https://placehold.co/800/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.Parse("d25405b3-fc30-4fd4-a8bb-cbbb4ef259f4"), Title = "Image 9",
                Content = GetImageAsByteArrayAsync("https://placehold.co/900/png").Result, ContentType = "image/png",
                OwnerId = userId.ToString()
            },
            new Image
            {
                Id = Guid.Parse("fcc40f61-b7cd-4813-88c0-4d7f907a9b19"), Title = "Image 10",
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

