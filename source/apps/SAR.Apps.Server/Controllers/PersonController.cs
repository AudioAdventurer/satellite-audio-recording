using System;
using System.Collections;
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
        public ActionResult<List<Person>> GetPeople(
            [FromRoute] Guid projectId)
        {
            var response = _projectService.GetPeople(
                User.GetPersonId(),
                projectId);

            return Ok(response);
        }
        
        [HttpGet]
        [Route("api/projects/{projectId:Guid}/people/available")]
        public ActionResult<List<Person>> GetAvailablePeople(
            [FromRoute] Guid projectId)
        {
            var response = _projectService.GetAvailablePeople(
                User.GetPersonId(),
                projectId);

            return Ok(response);
        }

        [HttpGet]
        [Route("api/projects/{projectId:Guid}/people/{personId:Guid}")]
        public ActionResult<Person> GetPerson(
            [FromRoute] Guid projectId,
            [FromRoute] Guid personId)
        {
            var response = _projectService.GetPerson(
                User.GetPersonId(),
                projectId,
                personId);

            return Ok(response);
        }
        
        [HttpGet]
        [Route("api/people/self")]
        public ActionResult<Person> GetSelf()
        {
            var response = _projectService.GetPerson(
                User.GetPersonId());

            return Ok(response);
        }
        
        [HttpPost]
        [Route("api/people/self")]
        public ActionResult SaveSelf(
            [FromBody] Profile profile)
        {
            _projectService.SaveProfile(
                User.GetPersonId(),
                profile);

            return Ok();
        }

        [HttpGet]
        [Route("api/access/self")]
        public ActionResult<IEnumerable<ProjectAccess>> GetSelfAccess()
        {
            var access = _projectService.GetAccessRights(
                User.GetPersonId());

            return Ok(access);
        }
        
        [HttpGet]
        [Route("api/people/{personId:Guid}")]
        public ActionResult<Person> GetPerson(
            [FromRoute] Guid personId)
        {
            var response = _projectService.GetPerson(
                User.GetUserId(),
                personId);

            return Ok(response);
        }

        [HttpGet]
        [Route("api/projects/{projectId:Guid}/ui/people/{personId:Guid}")]
        public ActionResult<PersonWithAccess> GetPersonWithAccess(
            [FromRoute] Guid projectId,
            [FromRoute] Guid personId)
        {
            var response = _projectService.GetPersonWithAccess(
                User.GetPersonId(),
                projectId,
                personId);

            return Ok(response);
        }


        [HttpGet]
        [Route("api/projects/{projectId:Guid}/ui/people")]
        public ActionResult<List<PersonWithAccess>> GetPeopleWithAccess(
            [FromRoute] Guid projectId)
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

        [HttpPost]
        [Route("api/projects/{projectId:Guid}/people")]
        public ActionResult SaveParticipantAccess(
            [FromRoute] Guid projectId,
            [FromBody] Participant participant)
        {
            _projectService.SaveProjectAccess(
                User.GetPersonId(),
                projectId,
                participant.PersonId,
                participant.AccessTypes);

            return Ok();
        }
    }
}
