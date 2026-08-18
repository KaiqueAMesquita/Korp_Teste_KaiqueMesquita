namespace StockApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate _next)
    {
        next = _next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ArgumentException ex)
        {
            await HandleException(context, 400, ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            await HandleException(context, 404, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            await HandleException(context, 409, ex.Message);
        }
        catch (Exception)
        {
            await HandleException(
                context,
                500,
                "Ocorreu um erro interno no servidor."
            );
        }
    }

    private static async Task HandleException(
        HttpContext context,
        int status,
        string message)
    {
        context.Response.StatusCode = status;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            status,
            message
        });
    }
}