using Microsoft.AspNetCore.Identity;

namespace Images.Domain.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}

