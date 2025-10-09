using BookingSystemApi.Application.Extensions;
using BookingSystemApi.Application.Mapping;
using BookingSystemApi.Core.Entities;
using BookingSystemApi.Extensions;
using BookingSystemApi.Infrastructure.Auth;
using BookingSystemApi.Infrastructure.Extensions;
using BookingSystemApi.Middlewares;
using BookingSystemApi.Persistence;
using BookingSystemApi.Persistence.Configurations;
using BookingSystemApi.Persistence.Extensions;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfiguration();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddApiAuthentication(builder.Configuration);

builder.Services.AddAutoMapper(configuration => configuration
    .AddProfile<BookingSystemProfile>(), typeof(Program));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    Secure = CookieSecurePolicy.Always,
    HttpOnly = HttpOnlyPolicy.Always,
});

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedRolesAsync(roleManager);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
