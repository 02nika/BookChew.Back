using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Dtos.Policies;
using Shared.Dtos.User;

namespace BookChew.Api.Extensions;

public static class Endpoints
{
    public static void RestaurantsEndpoints(this WebApplication app)
    {
        const string tag = "Restaurant";

        app.MapGet("/api/restaurants", (HttpContext context, IServiceManager serviceManager) => Results.Ok())
            .RequireAuthorization(PolicyData.AdminPolicyName)
            .WithTags(tag);
    }

    public static void UsersEndpoints(this WebApplication app)
    {
        const string tag = "User";

        app.MapPost("/api/user/register", [Authorize] async (IServiceManager serviceManager, [FromBody] AddUserDto addUserDto) =>
        {
            await serviceManager.UserService.AddUserAsync(addUserDto);
            var response = serviceManager.AuthService.Auth();

            return Results.Ok(response);
        }).WithTags(tag);

        app.MapPost("/api/user/login", async (IServiceManager serviceManager, [FromBody] LoginUserDto userDto) =>
        {
            var exists = await serviceManager.UserService.UserExistsAsync(userDto);
            if (!exists) return Results.NotFound();

            var response = serviceManager.AuthService.Auth();
            return Results.Ok(response);
        }).WithTags(tag);
    }

    public static void AuthEndpoints(this WebApplication app)
    {
        const string tag = "Auth";

        app.MapPost("/api/auth/refresh", (HttpContext context, IServiceManager serviceManager) =>
        {
            var tokenHash = context.Request.Headers["refresh"];
            serviceManager.AuthService.TokenIsValid(tokenHash.First()!);
            
            var response = serviceManager.AuthService.Auth();
            return Results.Ok(response);
        }).WithTags(tag);
    }
}