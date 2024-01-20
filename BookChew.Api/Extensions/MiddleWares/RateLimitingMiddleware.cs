using Shared.Extensions;

namespace BookChew.Api.Extensions.MiddleWares;

public class RateLimitingMiddleware(RequestDelegate next)
{
    private const int RequestCountLimit = 50;
    private const int RequestTimeLimit = 60 * 3;
    
    private static readonly Dictionary<string, RequestTrack> RequestTracker = new();

    public async Task InvokeAsync(HttpContext context)
    {
        var clientKey = context.Request.HttpContext
            .Connection.RemoteIpAddress?.ToString();

        RemoveOldItems();
        
        AddRequestTracker(clientKey!);
        
        if (HasTooMany(clientKey!))
        {
            await ReturnExceeded(context);
            return;
        }

        await next(context);
    }

    private static void RemoveOldItems()
    {
        var keysToRemove = RequestTracker
            .Where(pair => DateTime.Now.Subtract(pair.Value.FirstTime).TotalSeconds > RequestTimeLimit)
            .Select(pair => pair.Key)
            .ToList();

        foreach (var key in keysToRemove) 
            RequestTracker.Remove(key);
    }

    private static void AddRequestTracker(string clientKey)
    {
        if (RequestTracker.TryGetValue(clientKey, out var value))
        {
            value.RequestCount++;
            return;
        }
        
        RequestTracker[clientKey] = new RequestTrack
        {
            FirstTime = DateTime.Now,
            RequestCount = 1
        };
    }
    
    private static bool HasTooMany(string clientKey)
    {
        return RequestTracker[clientKey].RequestCount > RequestCountLimit;
    }
    
    private static async Task ReturnExceeded(HttpContext context)
    {
        context.Response.StatusCode = 429;
        await context.Response.WriteAsync("RATE_LIMIT_EXCEEDED");
    }
}