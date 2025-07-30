using Microsoft.AspNetCore.Http;

namespace InfraConnect.Application.IServices.Auth
{
    public interface ICookieService
    {
        void SetJwtCookie(HttpResponse response, string token, TimeSpan expiration);
        void ClearJwtCookie(HttpResponse response);
    }
}