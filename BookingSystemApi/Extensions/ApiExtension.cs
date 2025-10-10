using System.Text;
using BookingSystemApi.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookingSystemApi.Extensions;

public static class ApiExtension
{
    public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions!.Issuer,
                    ValidAudience = jwtOptions!.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var authOptions = context.HttpContext
                            .RequestServices
                            .GetRequiredService<IOptions<AuthOptions>>()
                            .Value;

                        var cookieName = authOptions.CookieName;

                        context.Token = context.Request.Cookies[cookieName];

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization();
    }
}