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
        private readonly ElementRepo _elementRepo;
        private readonly PersonRepo _personRepo;
        private readonly ProjectAccessRepo _projectAccessRepo;
        private readonly ProjectRepo _projectRepo;
        private readonly SceneRepo _sceneRepo;

        public ScriptService(
            CharacterRepo characterRepo,
            ElementRepo elementRepo,
            PersonRepo personRepo,
            ProjectAccessRepo projectAccessRepo,
            ProjectRepo projectRepo,
            SceneRepo sceneRepo)
        {
            _characterRepo = characterRepo;
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

        public void Save(Scene scene)
        {
            _sceneRepo.Save(scene);
        }

        public void Save(Element element)
        {
            _elementRepo.Save(element);
        }

        public void Save(Character character)
        {
            _characterRepo.Save(character);
        }

        public IEnumerable<Scene> GetScenesByProject(Guid projectId)
        {
            return _sceneRepo.GetByProject(projectId);
        }

        public IEnumerable<Character> GetCharactersByProject(Guid projectId)
        {
            return _characterRepo.GetByProject(projectId);
        }

        public IEnumerable<Element> GetElementsByScene(Guid sceneId)
        {
            return _elementRepo.GetByScene(sceneId);
        }

        public void DeleteElement(Guid elementId)
        {
            _elementRepo.Delete(elementId);
        }

        public void DeleteElementsByScene(Guid sceneId)
        {
            _elementRepo.Delete(sceneId);
        }

        public void DeleteScene(Guid sceneId)
        {
            _sceneRepo.Delete(sceneId);
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

        public void DeleteProjectScript(Guid projectId)
        {
            //Get All Scenes
            var scenes = GetScenesByProject(projectId);
            foreach (var scene in scenes)
            {
                //Delete Elements for the scene
                DeleteElementsByScene(scene.Id);

                //Delete the scene
                DeleteScene(scene.Id);
            }

            //Delete All Characters
            DeleteCharactersByProject(projectId);
        }
    }
}
