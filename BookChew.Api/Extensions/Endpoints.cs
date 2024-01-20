using BookChew.Api.Extensions.MiddleWares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Dto.Policies;
using Shared.Dto.Restaurant;
using Shared.Dto.User;

namespace BookChew.Api.Extensions;

public static class Endpoints
{
    public static void RestaurantsEndpoints(this WebApplication app)
    {
        const string tag = "Restaurant";

        app.MapGet("/api/restaurants",
                [Authorize] async (HttpContext context, IServiceManager serviceManager) =>
                    await serviceManager.RestaurantService.RestaurantsAsync())
            .AddEndpointFilter<RestaurantsFilter>()
            .RequireAuthorization(PolicyData.AdminPolicyName)
            .WithTags(tag);

        app.MapPost("/api/restaurant",
                [Authorize] async (HttpContext context, IServiceManager serviceManager,
                    [FromBody] AddRestaurantDto restaurant) =>
                {
                    var tokenHash = context.Request.Headers["Authorization"];
                    var userId = serviceManager.AuthService.GetUserId(tokenHash.First()!.Split(' ')[1]);
                    
                    await serviceManager.RestaurantService.AddRestaurantAsync(restaurant, userId);

                    return Results.Ok();
                })
            .AddEndpointFilter<RestaurantsFilter>()
            .RequireAuthorization(PolicyData.AdminPolicyName)
            .WithTags(tag);
    }

    public static void UsersEndpoints(this WebApplication app)
    {
        const string tag = "User";

        app.MapPost("/api/user/register",
            async (IServiceManager serviceManager, [FromBody] AddUserDto addUserDto) =>
            {
                var user = await serviceManager.UserService.AddUserAsync(addUserDto);
                var response = serviceManager.AuthService.Auth(user.Id);

                return Results.Ok(response);
            })
            .WithTags(tag);

        app.MapPost("/api/user/login", async (IServiceManager serviceManager, [FromBody] LoginUserDto userDto) =>
        {
            var u = await serviceManager.UserService.GetUserAsync(userDto);
            var response = serviceManager.AuthService.Auth(u.Id);
            return Results.Ok(response);
        }).WithTags(tag);
    }

    public static void AuthEndpoints(this WebApplication app)
    {
        const string tag = "Auth";

        app.MapPost("/api/auth/refresh", (HttpContext context, IServiceManager serviceManager) =>
        {
            var tokenHash = context.Request.Headers["refresh"];
            var userId = serviceManager.AuthService.GetUserId(tokenHash.First()!);

            var response = serviceManager.AuthService.Auth(userId);
            return Results.Ok(response);
        }).WithTags(tag);
    }
}