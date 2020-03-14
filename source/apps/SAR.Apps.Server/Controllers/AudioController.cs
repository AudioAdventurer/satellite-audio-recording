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
    public class AudioController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly ISarLogger _logger;

        public AudioController(
            ProjectService projectService,
            ISarLogger logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/projects/{projectId:Guid}/audiosummary/scenes")]
        public ActionResult<IEnumerable<SceneAudioSummary>> GetSceneAudioSummary([FromRoute] Guid projectId)
        {
            _logger.Info("Getting Scene Audio Summary");
            var response = _projectService.GetSceneAudioSummary(
                User.GetPersonId(),
                projectId);
            _logger.Info("Completed Scene Audio Summary");
            return Ok(response);
        }
        
        [HttpGet]
        [Route("api/projects/{projectId:Guid}/audiosummary/characters")]
        public ActionResult<IEnumerable<CharacterAudioSummary>> GetCharacterAudioSummary([FromRoute] Guid projectId)
        {
            _logger.Info("Getting Character Audio Summary");
            var response = _projectService.GetCharacterAudioSummary(
                User.GetPersonId(),
                projectId);
            _logger.Info("Completed Character Audio Summary");
            return Ok(response);
        }
        
        [HttpPost]
        [Route("api/projects/{projectId:Guid}/recalcaudio")]
        public ActionResult<IEnumerable<SceneAudioSummary>> RecalcAudioLines([FromRoute] Guid projectId)
        {
            _projectService.RecalcAudioLines(
                User.GetPersonId(),
                projectId);

            return Ok();
        }
    }
}