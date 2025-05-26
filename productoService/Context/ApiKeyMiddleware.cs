namespace productoService.Context
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "X-API-KEY";
        private const string ApiKey = "123456";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKey) || apiKey != ApiKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key no válida");
                return;
            }

            await _next(context);
        }
    }
}
