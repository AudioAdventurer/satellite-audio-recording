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
        private readonly CharacterRepo _characterRepo;
        private readonly ScriptElementRepo _scriptElementRepo;
        private readonly PersonRepo _personRepo;
        private readonly ProjectAccessRepo _projectAccessRepo;
        private readonly ProjectRepo _projectRepo;

        public ScriptService(
            CharacterRepo characterRepo,
            ScriptElementRepo scriptElementRepo,
            PersonRepo personRepo,
            ProjectAccessRepo projectAccessRepo,
            ProjectRepo projectRepo)
        {
            _characterRepo = characterRepo;
            _scriptElementRepo = scriptElementRepo;
            _personRepo = personRepo;
            _projectAccessRepo = projectAccessRepo;
            _projectRepo = projectRepo;
        }

        public Person GetPerson(Guid personId)
        {
            return _personRepo.GetById(personId);
        }

        public IEnumerable<Person> GetPeople()
        {
            return _personRepo.GetAll();
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
            var access = _projectAccessRepo.Get(personId, projectId);

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

        public ProjectAccess GetProjectAccess(Guid personId, Guid projectId)
        {
            return _projectAccessRepo.Get(personId, projectId);
        }

        public IEnumerable<ProjectAccess> GetProjectAccessByProject(Guid projectId)
        {
            return _projectAccessRepo.GetByProject(projectId);
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

        public void Save(ScriptElement element)
        {
            _scriptElementRepo.Save(element);
        }

        public ScriptElement GetScriptElement(Guid elementId)
        {
            return _scriptElementRepo.GetById(elementId);
        }

        public IEnumerable<ScriptElement> GetScriptElements(Guid projectId, int start, int end)
        {
            return _scriptElementRepo.GetByProject(projectId, start, end);
        }

        public void Save(Character character)
        {
            _characterRepo.Save(character);
        }

        public IEnumerable<Character> GetCharactersByProject(Guid projectId)
        {
            return _characterRepo.GetByProject(projectId);
        }

        public Character GetCharacter(Guid characterId)
        {
            return _characterRepo.GetById(characterId);
        }

        public void DeleteScriptElement(Guid elementId)
        {
            _scriptElementRepo.Delete(elementId);
        }

        public void DeleteCharacter(Guid characterId)
        {
            _characterRepo.Delete(characterId);
        }

        public void DeleteProjectAccess(Guid projectAccessId)
        {
            _projectAccessRepo.Delete(projectAccessId);
        }

        public void DeleteProject(Guid projectId)
        {
            _projectRepo.Delete(projectId);
        }

        public void DeleteCharactersByProject(Guid projectId)
        {
            _characterRepo.DeleteByProject(projectId);
        }

        public void DeleteProjectAccessByProject(Guid projectId)
        {
            _projectAccessRepo.DeleteByProject(projectId);
        }

        public void DeleteScriptElementsByProject(Guid projectId)
        {
            _scriptElementRepo.DeleteByProject(projectId);
        }

        public void DeleteProjectScript(Guid projectId)
        {
            //Delete Elements for the scene
            DeleteScriptElementsByProject(projectId);

            //Delete All Characters
            DeleteCharactersByProject(projectId);
        }
    }
}
