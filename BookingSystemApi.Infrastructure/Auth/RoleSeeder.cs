using BookingSystemApi.Core.Constants;
using Microsoft.AspNetCore.Identity;

namespace BookingSystemApi.Infrastructure.Auth;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { Roles.Admin };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}