namespace ActivoFijo.Middlewares
{
    public class RequestInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            var usuario = "Admin"; //context.User.Identity.Name; // Asumiendo que estás usando autenticación
            var fecha = DateTime.UtcNow;

            context.Items["IpAddress"] = ipAddress;
            context.Items["Usuario"] = usuario;
            context.Items["Fecha"] = fecha;

            await _next(context);
        }
    }
}
