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

        public bool HasAccessToProject(Guid personId, Guid projectId)
        {
            return _scriptService.HasAccessToProject(personId, projectId);
        }

        public Project GetProject(Guid personId, Guid projectId)
        {
            if (HasAccessToProject(personId, projectId))
            {
                return _scriptService.GetProject(projectId);
            }

            throw new UnauthorizedAccessException();
        }

        public void SaveProject(Guid personId, Project project)
        {
            if (HasAccessToProject(personId, project.Id))
            {
                _scriptService.Save(project);
                return;
            }

            throw new UnauthorizedAccessException();
        }
    }
}
