namespace ActivoFijo.Services.IServices
{
    public interface IUserContextService
    {
        string GetUserName();
        string GetIpAddress();
        DateTime? GetRequestDate();
    }
}
