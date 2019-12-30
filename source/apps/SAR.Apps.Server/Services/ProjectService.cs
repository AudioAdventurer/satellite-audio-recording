using System;
using System.Collections.Generic;
using SAR.Modules.Script.Constants;
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
                //TODO - Validate Write Access
                _scriptService.Save(project);
                return;
            }

            throw new UnauthorizedAccessException();
        }

        public void DeleteProject(Guid personId, Guid projectId)
        {
            if (HasAccessToProject(personId, projectId))
            {
                //TODO - Validate Write Access (Owner?)

                //Delete all children of the project
                _scriptService.DeleteProjectScript(projectId);

                //Delete the access rights
                _scriptService.DeleteProjectAccessByProject(projectId);

                //Delete the Project
                _scriptService.DeleteProject(projectId);
            }
        }

        public void DeleteProjectScript(Guid personId, Guid projectId)
        {
            if (HasAccessToProject(personId, projectId))
            {
                //TODO - Validate Write Access (Owner?)

                //Delete all children of the project
                _scriptService.DeleteProjectScript(projectId);
            }
        }

        public bool IsOwner(Guid personId, Guid projectId)
        {
            var projectAccess = _scriptService.GetProjectAccess(personId, projectId);

            if (projectAccess != null)
            {
                if (projectAccess.AccessTypes.Contains(AccessTypes.Owner))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
