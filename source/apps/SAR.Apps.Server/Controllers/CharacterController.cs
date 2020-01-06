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
    public class CharacterController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly ISarLogger _logger;

        public CharacterController(
            ProjectService projectService,
            ISarLogger logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/projects/{projectId:Guid}/characters")]
        public ActionResult<List<Character>> GetCharacters(
            [FromRoute] Guid projectId)
        {
            var response = _projectService.GetCharacters(
                User.GetPersonId(), 
                projectId);

            return Ok(response);
        }

        [HttpGet]
        [Route("api/projects/{projectId:Guid}/characters/{characterId:Guid}")]
        public ActionResult<List<Character>> GetCharacter(
            [FromRoute] Guid projectId, 
            [FromRoute] Guid characterId)
        {
            var response = _projectService.GetCharacter(
                User.GetPersonId(),
                projectId,
                characterId);

            return Ok(response);
        }

        [HttpGet]
        [Route("api/projects/{projectId:Guid}/ui/characters")]
        public ActionResult<List<UICharacter>> GetCharactersWithPerformer(
            [FromRoute] Guid projectId)
        {
            var response = _projectService.GetUICharacters(
                User.GetPersonId(),
                projectId);

            return Ok(response);
        }

        [HttpPost]
        [Route("api/projects/{projectId:Guid}/characters")]
        public ActionResult SaveCharacter(
            [FromRoute] Guid projectId, 
            [FromBody] Character character)
        {
            if (character.ProjectId != projectId)
            {
                character.ProjectId = projectId;
            }

            _projectService.SaveCharacter(
                User.GetPersonId(),
                character);

            return Ok();
        }
    }
}
