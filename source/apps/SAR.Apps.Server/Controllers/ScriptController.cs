using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAR.Apps.Server.Helpers;
using SAR.Apps.Server.Objects;
using SAR.Apps.Server.Services;
using SAR.Libraries.Common.Interfaces;
using SAR.Modules.Script.Objects;

namespace SAR.Apps.Server.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class ScriptController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly ISarLogger _logger;

        public ScriptController(
            ProjectService projectService,
            ISarLogger logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/projects/{projectId:Guid}/script/{start:int}/{end:int}")]
        public ActionResult<IEnumerable<ScriptLine>> GetScript(
            [FromRoute] Guid projectId,
            [FromRoute] int start,
            [FromRoute] int end)
        {
            var response = _projectService.GetScript(
                User.GetPersonId(),
                projectId,
                start,
                end);

            return Ok(response);
        }
    }
}
