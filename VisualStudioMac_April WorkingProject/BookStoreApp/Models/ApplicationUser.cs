using Microsoft.AspNetCore.Identity;

namespace BookStoreApp;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
