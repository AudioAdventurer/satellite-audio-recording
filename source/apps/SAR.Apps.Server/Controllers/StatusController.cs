using Microsoft.AspNetCore.Mvc;
using SAR.Libraries.Common.Interfaces;

namespace SAR.Apps.Server.Controllers
{
    [Produces("text/plain")]
    [Route("/api")]
    public class StatusController : ControllerBase
    {
        private readonly ISarLogger _logger;

        public StatusController(
            ISarLogger logger)
        {
            _logger = logger;
        }

        public string Get()
        {
            _logger.Info("SAR Status Controller Called");
            return "SAR Api Server";
        }
    }
}
