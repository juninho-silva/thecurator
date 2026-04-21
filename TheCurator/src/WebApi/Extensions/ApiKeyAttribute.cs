using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    private const string HEADER_NAME = "X-API-Key";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var config = context.HttpContext.RequestServices.GetService<IConfiguration>();
        var apiKey = config["ApiKey"];

        if (!context.HttpContext.Request.Headers.TryGetValue(HEADER_NAME, out var extractedKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!apiKey.Equals(extractedKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await next();
    }
}