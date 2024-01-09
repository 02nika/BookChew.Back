using BookChew.Api.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Shared.Config;

var builder = WebApplication.CreateBuilder(args);
var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

builder.Services.ConfigureCors();
builder.Services.ConfigureServiceManager();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.ConfigureJwtAuthentication(jwt!);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomSwaggerGen();

var app = builder.Build();

app.Services.GetRequiredService<ILogger<Program>>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookChew.Api");
    });
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.AuthEndpoints();
app.Other();

app.Run();