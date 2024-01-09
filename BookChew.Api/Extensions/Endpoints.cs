using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Dtos;

namespace BookChew.Api.Extensions;

public static class Endpoints
{
    public static void AuthEndpoints(this WebApplication app)
    {
        app.MapPost("/api/auth", (IServiceManager serviceManager, [FromBody] AuthRequest authRequest) =>
        {
            var token = serviceManager.AuthService.AuthAsync(authRequest);
            return Results.Ok(token);
        });
    }

    public static void Other(this WebApplication app)
    {
        app.MapGet("api/weathers", [Authorize] () => "OK")
            .WithName("GetWeatherForecast")
            .WithOpenApi();
    }
}
