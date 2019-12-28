using System;
using System.Collections.Generic;
using SAR.Modules.Script.Objects;
using SAR.Modules.Script.Services;

namespace SAR.Apps.Server.Services
{
    public class ProjectService
    {
        private readonly ScriptService _scriptService;

        public ProjectService(
            ScriptService scriptService)
        {
            _scriptService = scriptService;
        }

        public IEnumerable<Project> GetProjects(Guid personId)
        {
            return _scriptService.GetProjectsByPerson(personId);
        }
    }
}
