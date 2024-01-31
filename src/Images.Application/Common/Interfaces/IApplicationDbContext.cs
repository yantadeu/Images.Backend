using Images.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Images.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Image> Images { get; set; }
    DbSet<ApplicationUser> ApplicationUsers { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}