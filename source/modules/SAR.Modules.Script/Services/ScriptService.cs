using System;
using System.Collections.Generic;
using System.Linq;
using SAR.Modules.Script.Objects;
using SAR.Modules.Script.Repos;
using SAR.Modules.Server.Repos;

namespace SAR.Modules.Script.Services
{
    public class ScriptService
    {
        private readonly ElementRepo _elementRepo;
        private readonly PersonRepo _personRepo;
        private readonly ProjectAccessRepo _projectAccessRepo;
        private readonly ProjectRepo _projectRepo;
        private readonly SceneRepo _sceneRepo;

        public ScriptService(
            ElementRepo elementRepo,
            PersonRepo personRepo,
            ProjectAccessRepo projectAccessRepo,
            ProjectRepo projectRepo,
            SceneRepo sceneRepo)
        {
            _elementRepo = elementRepo;
            _personRepo = personRepo;
            _projectAccessRepo = projectAccessRepo;
            _projectRepo = projectRepo;
            _sceneRepo = sceneRepo;
        }

        public Person GetPerson(Guid personId)
        {
            return _personRepo.GetById(personId);
        }

        public IEnumerable<Project> GetProjectsByPerson(Guid personId)
        {
            Dictionary<Guid, Project> projects = new Dictionary<Guid, Project>();

            var access = _projectAccessRepo.GetByPerson(personId);

            foreach (var projectAccess in access)
            {
                if (!projects.ContainsKey(projectAccess.ProjectId))
                {
                    var project = _projectRepo.GetById(projectAccess.ProjectId);

                    projects.Add(project.Id, project);
                }
            }

            return projects.Values.ToList();
        }

        public bool HasAccessToProject(Guid personId, Guid projectId)
        {
            var access = _projectAccessRepo.Get(projectId, personId);

            if (access != null)
            {
                return true;
            }

            return false;
        }

        public Project GetProject(Guid projectId)
        {
            return _projectRepo.GetById(projectId);
        }

        public void Save(Project project)
        {
            _projectRepo.Save(project);
        }

        public void Save(Person person)
        {
            _personRepo.Save(person);
        }

        public void Save(ProjectAccess projectAccess)
        {
            _projectAccessRepo.Save(projectAccess);
        }
    }
}
