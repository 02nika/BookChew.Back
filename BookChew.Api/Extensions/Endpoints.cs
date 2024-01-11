using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Dtos.Auth;
using Shared.Dtos.User;

namespace BookChew.Api.Extensions;

public static class Endpoints
{
    public static void AuthEndpoints(this WebApplication app)
    {
        app.MapPost("/api/auth", (IServiceManager serviceManager, [FromBody] AuthRequest authRequest) =>
        {
            var token = serviceManager.AuthService.AuthAsync(authRequest);
            return Results.Ok(token);
        }).WithTags("Auth");
    }

    public static void RestaurantsEndpoints(this WebApplication app)
    {
        
    }
    
    public static void UsersEndpoints(this WebApplication app)
    {
        app.MapPost("/api/user", [Authorize] async (IServiceManager serviceManager, [FromBody] AddUserDto addUserDto) =>
        {
            await serviceManager.UserService.AddUserAsync(addUserDto);
            return Results.Ok();
        }).WithTags("User");
        
        app.MapPost("/api/user/fill", async (IServiceManager serviceManager) =>
        {
            await serviceManager.UserService.FillUsersAsync();
            return Results.Ok();
        }).WithTags("User");
    }
}
