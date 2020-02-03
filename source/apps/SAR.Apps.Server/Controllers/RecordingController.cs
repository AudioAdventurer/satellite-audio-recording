using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAR.Apps.Server.Helpers;
using SAR.Apps.Server.Services;
using SAR.Libraries.Common.Interfaces;
using SAR.Modules.Script.Objects;

namespace SAR.Apps.Server.Controllers
{
    public class RecordingController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly ISarLogger _logger;

        public RecordingController (
            ProjectService projectService,
            ISarLogger logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/projects/{projectId:Guid}/script/{characterDialogId:Guid}/recording")]
        public ActionResult SaveRecording(
            [FromRoute] Guid projectId,
            [FromRoute] Guid characterDialogId,
            [FromForm] IFormFile file)
        {
            Guid personId = User.GetPersonId();
            if (file != null
                && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);

                    ms.Position = 0;

                    _projectService.SaveRecording(personId, projectId, characterDialogId, ms);
                }
            }
            else
            {
                return BadRequest("File not supplied");
            }

            return Ok();
        }

        [HttpGet]
        [Route("api/projects/{projectId:Guid}/script/{characterDialogId:Guid}")]
        public ActionResult<IEnumerable<Recording>> GetRecordings(
            [FromRoute] Guid projectId,
            [FromRoute] Guid characterDialogId)
        {
            Guid personId = User.GetPersonId();
            var recordings = _projectService.GetRecordings(personId, projectId, characterDialogId);
            return Ok(recordings);
        }

        [HttpGet]
        [Route("api/projects/{projectId:Guid}/recordings/{recordingId:Guid}")]
        public ActionResult GetRecording(
            [FromRoute] Guid projectId,
            [FromRoute] Guid recordingId)
        {
            Guid personId = User.GetPersonId();
            using (var stream = _projectService.GetRecording(personId, projectId, recordingId))
            {
                Byte[] data = null;

                if (stream != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        data = memoryStream.ToArray();
                    }

                    return File(data, "audio/wav");
                }

                return NotFound();
            }
        }
    }
}
