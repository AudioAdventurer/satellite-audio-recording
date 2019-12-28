using SAR.Libraries.Common.Interfaces;
using SAR.Libraries.Common.Security;

namespace SAR.Apps.Server.Services
{
    public class JwtService
    {
        private readonly string _jwtSecret;

        public JwtService(string jwtSecret)
        {
            _jwtSecret = jwtSecret;
        }

        public string CreateJwt(IWebSession session)
        {
            return JwtHelper.CreateJwt(session, _jwtSecret, 1);
        }
    }
}
