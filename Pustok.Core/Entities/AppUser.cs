using Microsoft.AspNetCore.Identity;

namespace Pustok.Core.Entities;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; } = null!;
}