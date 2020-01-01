using SAR.Libraries.Common.Interfaces;

namespace SAR.Apps.Server.Objects
{
    public class LoginResponse
    {
        public string JWT { get; set; }

        public IWebSession Session { get; set; }
    }
}
