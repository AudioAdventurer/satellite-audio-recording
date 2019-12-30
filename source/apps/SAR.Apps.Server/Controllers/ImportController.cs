using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAR.Apps.Server.Helpers;
using SAR.Apps.Server.Services;
using SAR.Libraries.Common.Interfaces;
using SAR.Modules.Script.Importer.Importers;
using SAR.Modules.Script.Services;

namespace SAR.Apps.Server.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class ImportController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly FountainImporter _fountainImporter;
        private readonly ISarLogger _logger;

        public ImportController(
            ProjectService projectService,
            FountainImporter fountainImporter,
            ISarLogger logger)
        {
            _projectService = projectService;
            _fountainImporter = fountainImporter;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/projects/{projectId:Guid}/import/fountain")]
        public ActionResult GetProject([FromRoute] Guid projectId, [FromForm] IFormFile file)
        {
            Guid personId = User.GetPersonId();

            if (_projectService.IsOwner(personId, projectId))
            {
                var script = "";

                if (file != null
                    && file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);

                        ms.Position = 0;

                        using (StreamReader sr = new StreamReader(ms))
                        {
                            script = sr.ReadToEnd();
                        }
                    }
                }
                else
                {
                    return BadRequest("File not supplied");
                }

                if (!string.IsNullOrEmpty(script))
                {
                    _fountainImporter.Import(projectId, script);

                    return Ok();
                }
            }

            return Unauthorized("Only a project owner can perform this action");
        }
    }
}
