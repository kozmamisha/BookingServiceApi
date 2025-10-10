using BookingSystemApi.Application.Exceptions;
using BookingSystemApi.Application.Interfaces;
using BookingSystemApi.Core.Constants;
using BookingSystemApi.Core.Entities;
using BookingSystemApi.Infrastructure.Auth;
using BookingSystemApi.Infrastructure.Interfaces.Auth;
using BookingSystemApi.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookingSystemApi.Application.Services;

public class AuthService(
    UserManager<UserEntity> userManager, 
    SignInManager<UserEntity> signInManager,
    IJwtProvider jwtProvider) : IAuthService
{
    public async Task Register(string userName, string email, string password, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userName) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Username, email, or password cannot be empty.");
        }

        var existingUser = await userManager.FindByEmailAsync(email);
        if (existingUser is not null)
        {
            throw new ArgumentException("User with this email already exists.");
        }

        var user = new UserEntity
        {
            UserName = userName,
            Email = email
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, Roles.User);
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ArgumentException($"Failed to register user: {errors}");
        }
    }

    public async Task<string> Login(string email, string password, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Email or password cannot be empty.");
        }
        
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password.");

        var result = await signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
            throw new UnauthorizedAccessException("Invalid email or password.");

        var token = jwtProvider.GenerateToken(user);

        return token;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null)
            throw new EntityNotFoundException("User not found");

        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded)
            throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.Description)));
    }
}