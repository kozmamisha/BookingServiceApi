using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookingSystemApi.Core.Constants;
using BookingSystemApi.Core.Entities;
using BookingSystemApi.Infrastructure.Interfaces.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookingSystemApi.Infrastructure.Auth;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public string GenerateToken(UserEntity user)
    {
        Claim[] claims = [
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Role, user.Role)
        ];

        var signinCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            signingCredentials: signinCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
