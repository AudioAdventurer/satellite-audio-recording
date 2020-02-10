using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAR.Apps.Server.Helpers;
using SAR.Apps.Server.Objects;
using SAR.Apps.Server.Services;
using SAR.Libraries.Common.Interfaces;
using SAR.Modules.Server.Objects;

namespace SAR.Apps.Server.Controllers
{    
    [Authorize]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly ISarLogger _logger;

        public UserController(
            ProjectService projectService,
            ISarLogger logger)
        {
            _projectService = projectService;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("api/users")]
        public ActionResult<List<UserEdit>> GetUsers()
        {
            var personId = User.GetPersonId();

            try
            {
                var users = _projectService.GetUserEdits(personId);
                return Ok(users);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
        
        [HttpPost]
        [Route("api/users")]
        public ActionResult CreateUser([FromBody] CreateUserRequest userRequest)
        {
            var personId = User.GetPersonId();

            try
            {
                _projectService.CreateUser(personId, userRequest);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
        
        [HttpPost]
        [Route("api/users/{userId:Guid}")]
        public ActionResult SetPassword(
            [FromRoute] Guid userId,
            [FromBody] SetPasswordRequest setPassword)
        {
            var personId = User.GetPersonId();

            try
            {
                _projectService.SetPassword(personId, userId, setPassword.NewPassword);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}