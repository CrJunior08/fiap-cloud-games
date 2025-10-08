using FCG.Domain.Exceptions;

namespace FCG.Web.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro capturado pelo middleware.");

                context.Response.ContentType = "application/json";
                switch (ex)
                {
                    case ConflictException:
                        context.Response.StatusCode = StatusCodes.Status409Conflict;
                        break;

                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }


                var response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                    Detailed = _env.IsDevelopment() ? ex.StackTrace : null
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }

}