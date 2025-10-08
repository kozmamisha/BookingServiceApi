using BookingSystemApi.Application.Extensions;
using BookingSystemApi.Application.Mapping;
using BookingSystemApi.Middlewares;
using BookingSystemApi.Persistence.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

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

app.MapControllers();

app.Run();
