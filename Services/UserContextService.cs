using ActivoFijo.Services.IServices;

namespace ActivoFijo.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext?.Items["Usuario"]?.ToString();
        }

        public string GetIpAddress()
        {
            return _httpContextAccessor.HttpContext?.Items["IpAddress"]?.ToString();
        }

        public DateTime? GetRequestDate()
        {
            return (DateTime?)_httpContextAccessor.HttpContext?.Items["Fecha"];
        }
    }
}
