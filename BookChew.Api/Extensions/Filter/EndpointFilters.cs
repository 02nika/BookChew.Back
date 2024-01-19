namespace BookChew.Api.Extensions.Filter;

public abstract class EndpointFilters : IEndpointFilter
{
    private readonly string _methodName;

    protected EndpointFilters()
    {
        _methodName = GetType().Name;
    }
    
    public virtual async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        Console.WriteLine("{0} Before next", _methodName);
        var result = await next(context);
        Console.WriteLine("{0} After next", _methodName);
        
        return result;
    }
}
