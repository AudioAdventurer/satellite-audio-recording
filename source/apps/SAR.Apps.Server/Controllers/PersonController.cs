using System;
using System.Collections.Generic;
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
    public class PersonController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly ISarLogger _logger;

        public PersonController(
            ProjectService projectService,
            ISarLogger logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/projects/{projectId:Guid}/people")]
        public ActionResult<List<Person>> GetPeople([FromRoute] Guid projectId)
        {
            var response = _projectService.GetPeople(
                User.GetPersonId(),
                projectId);

            return Ok(response);
        }


        [HttpGet]
        [Route("api/projects/{projectId:Guid}/ui/people")]
        public ActionResult<List<PersonWithAccess>> GetPeopleWithAccess([FromRoute] Guid projectId)
        {
            var response = _projectService.GetPeopleWithAccess(
                User.GetPersonId(),
                projectId);

            return Ok(response);
        }

        [HttpGet]
        [Route("api/people")]
        public ActionResult<List<Person>> GetPeople()
        {
            var response = _projectService.GetPeopleInSystem(
                User.GetUserId());

            return Ok(response);
        }
    }
}
