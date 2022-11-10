namespace EnergiBolaget.Auth.Model;

using Microsoft.AspNetCore.Identity;

using System;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
