using Microsoft.AspNetCore.Http;

namespace TravelAgency.Helper
{
    public class SessionInfoSetup
    {
        //private IHttpContextAccessor _httpContextAccessor;
        //public SessionInfoSetup(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}
        public int GetUserIdFromSession()
        {
            return new HttpContextAccessor().HttpContext.Session.GetInt32("UserId").Value;
        }
        public int GetCompanyIdFromSession()
        {
            return new HttpContextAccessor().HttpContext.Session.GetInt32("CompanyId").Value;
        }
        public string IsAdmin()
        {
            return new HttpContextAccessor().HttpContext.Session.GetString("IsAdmin");
        }
    }
}
