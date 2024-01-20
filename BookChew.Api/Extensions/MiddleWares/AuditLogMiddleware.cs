namespace BookChew.Api.Extensions.MiddleWares;

public class AuditLogMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        LogAuditInformation(context);

        await next(context);
    }

    private static void LogAuditInformation(HttpContext context)
    {
        var username = context.User.Identity!.Name;
        var requestPath = context.Request.Path;
        var method = context.Request.Method;

        Console.WriteLine(
            $"Audit Log - User: {username}, Path: {requestPath}, Method: {method}, Timestamp: {DateTime.Now}");
    }
}