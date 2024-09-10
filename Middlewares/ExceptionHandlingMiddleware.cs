using System.Net;
using System.Text.Json;
using ActivoFijo.Exceptions;

namespace ActivoFijo.Middlewares
{
    public class ExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ResourceNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (ConflictException ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, HttpStatusCode.Conflict, ex.Message);
            }
            catch (BadRequestException ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción no manejada capturada en el middleware de manejo de excepciones.");
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Ocurrió un error en el servidor, intente más tarde");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorDetails = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));
        }

    }
}
