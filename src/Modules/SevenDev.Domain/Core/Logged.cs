using SevenDev.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace SevenDev.Domain.Core
{
    public class Logged : ILogged
    {
        private readonly IHttpContextAccessor _accessor;

        public Logged(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public int GetUserLoggedId()
        {
            var id = _accessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "jti").Value;

            return int.Parse(id);
        }
    }
}
