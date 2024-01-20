using BookChew.Api.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Service.Mapper;
using Shared.Config;

var builder = WebApplication.CreateBuilder(args);
var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

builder.Services.ConfigureCors();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.ConfigureJwtAuthentication(jwt!);
builder.Services.ConfigureJwtAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

app.Services.GetRequiredService<ILogger<Program>>();

app.ConfigureRateLimitingHandler();
app.ConfigureExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookChew.Api");
    });
    app.UseMigrationsEndPoint();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UsersEndpoints();
app.RestaurantsEndpoints();
app.AuthEndpoints();

app.Run();