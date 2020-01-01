using System;
using Microsoft.AspNetCore.Mvc;
using SAR.Apps.Server.Objects;
using SAR.Apps.Server.Services;
using SAR.Libraries.Common.Interfaces;

namespace SAR.Apps.Server.Controllers
{
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;
        private readonly ISarLogger _logger;

        public AuthController(
            AuthService authService,
            JwtService jwtService,
            ISarLogger logger)
        {
            _authService = authService;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest loginRequest)
        {
            var webSession = _authService.CreateSession(loginRequest.Email, loginRequest.Password);

            if (webSession != null)
            {
                var jwt = _jwtService.CreateJwt(webSession);
                var response = new LoginResponse
                {
                    JWT = jwt,
                    Session = webSession
                };

                return Ok(response);
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("api/logout/{sessionId:Guid}")]
        public ActionResult Logout([FromRoute] Guid sessionId)
        {
            _authService.CloseSession(sessionId);
            return Ok();
        }
    }
}
