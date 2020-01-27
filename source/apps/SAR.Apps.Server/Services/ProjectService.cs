using System;
using System.Collections.Generic;
using System.Linq;
using SAR.Apps.Server.Objects;
using SAR.Libraries.Fountain.Objects;
using SAR.Modules.Script.Constants;
using SAR.Modules.Script.Helpers;
using SAR.Modules.Script.Objects;
using SAR.Modules.Script.Services;
using SAR.Modules.Server.Constants;
using SAR.Modules.Server.Services;

namespace SAR.Apps.Server.Services
{
    public class ProjectService
    {
        private readonly ScriptService _scriptService;
        private readonly ServerService _serverService;

        public ProjectService(
            ScriptService scriptService,
            ServerService serverService)
        {
            _scriptService = scriptService;
            _serverService = serverService;
        }

        public IEnumerable<Project> GetProjects(Guid userPersonId)
        {
            return _scriptService.GetProjectsByPerson(userPersonId);
        }

        public bool HasAccessToProject(Guid userPersonId, Guid projectId)
        {
            return _scriptService.HasAccessToProject(userPersonId, projectId);
        }

        public Project GetProject(Guid userPersonId, Guid projectId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                return _scriptService.GetProject(projectId);
            }

            throw new UnauthorizedAccessException();
        }

        public void SaveProject(Guid userPersonId, Project project)
        {
            if (HasAccessToProject(userPersonId, project.Id))
            {
                //TODO - Validate Write Access
                _scriptService.Save(project);
                return;
            }

            throw new UnauthorizedAccessException();
        }

        public void DeleteProject(Guid userPersonId, Guid projectId)
        {
            if (IsProjectOwner(userPersonId, projectId))
            {
                //TODO - Validate Write Access (Owner?)

                //Delete all children of the project
                _scriptService.DeleteProjectScript(projectId);

                //Delete the access rights
                _scriptService.DeleteProjectAccessByProject(projectId);

                //Delete the Project
                _scriptService.DeleteProject(projectId);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }


        }

        public void DeleteProjectScript(Guid userPersonId, Guid projectId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                //TODO - Validate Write Access (Owner?)

                //Delete all children of the project
                _scriptService.DeleteProjectScript(projectId);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public bool IsProjectOwner(Guid personId, Guid projectId)
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

        public IEnumerable<Character> GetCharacters(Guid userPersonId, Guid projectId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                return _scriptService.GetCharactersByProject(projectId);
            }

            throw new UnauthorizedAccessException();
        }

        

        public Character GetCharacter(Guid userPersonId, Guid projectId, Guid characterId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                var character = _scriptService.GetCharacter(characterId);
                return character;
            }

            throw new UnauthorizedAccessException();
        }

        public void SaveCharacter(Guid userPersonId, Character character)
        {
            if (HasAccessToProject(userPersonId, character.ProjectId))
            {
                _scriptService.Save(character);
                return;
            }

            throw new UnauthorizedAccessException();
        }

        public UICharacter BuildUICharacter(Character character)
        {
            var uic = new UICharacter(character);

            if (character.PerformerPersonId != null)
            {
                uic.Performer = _scriptService.GetPerson(character.PerformerPersonId.Value);
            }

            return uic;
        }

        public IEnumerable<UICharacter> GetUICharacters(Guid userPersonId, Guid projectId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                var output = new List<UICharacter>();

                var characters = _scriptService.GetCharactersByProject(projectId);
                foreach (var character in characters)
                {
                    output.Add(BuildUICharacter(character));
                }

                return output;
            }

            throw new UnauthorizedAccessException();
        }

        public UICharacter GetUICharacter(Guid userPersonId, Guid projectId, Guid characterId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                var output = new List<UICharacter>();

                var character = _scriptService.GetCharacter(characterId);
                return BuildUICharacter(character);
            }

            throw new UnauthorizedAccessException();
        }

        public IEnumerable<Person> GetPeople(Guid userPersonId, Guid projectId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                var temp = new Dictionary<Guid, Person>();

                var accessRights = _scriptService.GetProjectAccessByProject(projectId);
                foreach (var projectAccess in accessRights)
                {
                    if (!temp.ContainsKey(projectAccess.PersonId))
                    {
                        var person = _scriptService.GetPerson(projectAccess.PersonId);
                        temp.Add(person.Id, person);
                    }
                }

                return temp.Values.ToList();
            }

            throw new UnauthorizedAccessException();
        }

        public Person GetPerson(Guid userPersonId, Guid projectId, Guid personId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                var temp = new Dictionary<Guid, Person>();

                var accessRight = _scriptService.GetProjectAccess(personId, projectId);

                if (accessRight != null)
                {
                    var person = _scriptService.GetPerson(personId);
                    return person;
                }

                return null;
            }

            throw new UnauthorizedAccessException();
        }

        public PersonWithAccess GetPersonWithAccess(Guid userPersonId, Guid projectId, Guid personId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                var temp = new Dictionary<Guid, Person>();

                var accessRight = _scriptService.GetProjectAccess(personId, projectId);

                if (accessRight != null)
                {

                    var person = _scriptService.GetPerson(personId);
                    var pwa = new PersonWithAccess(person);
                    pwa.AccessTypes.AddRange(accessRight.AccessTypes);

                    return pwa;
                }

                return null;
            }

            throw new UnauthorizedAccessException();
        }

        public bool IsSystemOwner(Guid userId)
        {
            var user = _serverService.GetUser(userId);

            if (user.UserType.Equals(UserTypes.Owner))
            {
                return true;
            }

            return false;
        }

        public bool IsSystemAdmin(Guid userId)
        {
            var user = _serverService.GetUser(userId);

            if (user.UserType.Equals(UserTypes.Owner)
                || user.UserType.Equals(UserTypes.Admin))
            {
                return true;
            }

            return false;
        }

        public IEnumerable<Person> GetPeopleInSystem(Guid userId)
        {
            if (IsSystemAdmin(userId))
            {
                return _scriptService.GetPeople();
            }

            throw new UnauthorizedAccessException();
        }

        public IEnumerable<PersonWithAccess> GetPeopleWithAccess(Guid userPersonId, Guid projectId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                var temp = new Dictionary<Guid, PersonWithAccess>();

                var accessRights = _scriptService.GetProjectAccessByProject(projectId);
                foreach (var projectAccess in accessRights)
                {
                    if (!temp.ContainsKey(projectAccess.PersonId))
                    {
                        var person = _scriptService.GetPerson(projectAccess.PersonId);
                        var pwa = new PersonWithAccess(person);
                        pwa.AccessTypes.AddRange(projectAccess.AccessTypes);

                        temp.Add(person.Id, pwa);
                    }
                }

                return temp.Values.ToList();
            }

            throw new UnauthorizedAccessException();
        }

        private UIScene BuildScene(SAR.Modules.Script.Objects.Scene scene)
        {
            if (scene == null)
            {
                return null;
            }

            var scriptElement = _scriptService.GetScriptElement(scene.ScriptElementId);
            var fs = (SceneElement)scriptElement.ToFountain();

            UIScene s = new UIScene
            {
                Id = scene.Id,
                InteriorExterior = fs.InteriorExterior,
                Location = fs.Location,
                SceneNumber = fs.SceneNumber,
                TimeOfDay = fs.TimeOfDay,
                SequenceNumber = scene.Number,
                ScriptPosition = scriptElement.SequenceNumber
            };

            return s;
        }

        public UIScene GetScene(Guid userPersonId, Guid projectId, Guid sceneId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                var scene = _scriptService.GetScene(sceneId);
                var s = BuildScene(scene);
                return s;
            }

            throw new UnauthorizedAccessException();
        }

        public IEnumerable<UIScene> GetScenes(Guid userPersonId, Guid projectId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                var scenes = _scriptService.GetScenesByProject(projectId);
                var output = new List<Objects.UIScene>();

                foreach (var scene in scenes)
                {
                    UIScene s = BuildScene(scene);
                    output.Add(s);
                }

                return output;
            }

            throw new UnauthorizedAccessException();
        }

        public IEnumerable<ScriptLine> GetScript(Guid userPersonId, Guid projectId, int start, int end)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                var elements =  _scriptService.GetScriptElements(projectId, start, end);

                var output = new List<ScriptLine>();

                foreach (var se in elements)
                {
                    output.Add(BuildScriptLine(se));
                }

                return output;
            }

            throw new UnauthorizedAccessException();
        }

        public CharacterDialog GetCharacterDialog(Guid userPersonId, Guid projectId, Guid characterDialogId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                return _scriptService.GetCharacterDialog(characterDialogId);
            }

            throw new UnauthorizedAccessException();
        }

        public IEnumerable<ScriptLine> GetScriptLinesByCharacter(Guid userPersonId, Guid projectId, Guid characterId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                var elements = _scriptService.GetCharacterDialogsByCharacter(characterId);

                var output = new List<ScriptLine>();

                foreach (var cdc in elements)
                {
                    var se = _scriptService.GetScriptElement(cdc.ScriptElementId);
                    var parts = se.FountainElementType.Split(".".ToCharArray());
                    var type = parts[parts.Length - 1].Replace("Element", "");

                    var sl = new ScriptLine
                    {
                        ProjectId = se.ProjectId,
                        Line = se.FountainRawData,
                        SequenceNumber = se.SequenceNumber,
                        LineType = type
                    };

                    output.Add(sl);
                }

                return output;
            }

            throw new UnauthorizedAccessException();
        }

        public ScriptLine GetNextScriptLineByCharacter(
            Guid userPersonId, 
            Guid projectId, 
            Guid characterId,
            int currentScriptSequenceNumber)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                var characterDialog = _scriptService.GetNextCharacterDialogByCharacter(characterId, currentScriptSequenceNumber);

                var sl = BuildScriptLine(characterDialog);
                return sl;
            }

            throw new UnauthorizedAccessException();
        }

        public IEnumerable<ScriptLine> GetNextScriptLinesByCharacter(
            Guid userPersonId,
            Guid projectId,
            Guid characterId,
            int currentScriptSequenceNumber,
            int limit)
        {
            Dictionary<Guid, UIScene> scenes = new Dictionary<Guid, UIScene>();

            if (HasAccessToProject(userPersonId, projectId))
            {
                var output = new List<ScriptLine>();
                var characterDialogs = _scriptService.GetNextCharacterDialogsByCharacter(characterId, currentScriptSequenceNumber, limit);

                foreach (var characterDialog in characterDialogs)
                {
                    var sl = BuildScriptLine(characterDialog, scenes);
                    output.Add(sl);
                }

                return output;
            }

            throw new UnauthorizedAccessException();
        }

        public ScriptLine GetPreviousScriptLineByCharacter(
            Guid userPersonId, 
            Guid projectId, 
            Guid characterId, 
            int currentScriptSequenceNumber)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                var characterDialog = _scriptService.GetPreviousCharacterDialogByCharacter(characterId, currentScriptSequenceNumber);

                var sl = BuildScriptLine(characterDialog);
                return sl;
            }

            throw new UnauthorizedAccessException();
        }

        private ScriptLine BuildScriptLine(
            CharacterDialog characterDialog,
            Dictionary<Guid, UIScene> scenes = null,
            ScriptElement scriptElement = null)
        {
            UIScene scene = null;

            if (characterDialog.SceneId.HasValue)
            {
                Guid sceneId = characterDialog.SceneId.Value;

                if (scenes != null)
                {
                    if (scenes.ContainsKey(sceneId))
                    {
                        scene = scenes[sceneId];
                    }
                    else
                    {
                        scene = BuildScene(_scriptService.GetScene(sceneId));
                        scenes.Add(sceneId, scene);
                    }
                }
                else
                {
                    scene = BuildScene(_scriptService.GetScene(sceneId));
                }
            }

            var se = scriptElement;
            if (se == null)
            {
                se = _scriptService.GetScriptElement(characterDialog.ScriptElementId);
            }
                
            var parts = se.FountainElementType.Split(".".ToCharArray());
            var type = parts[parts.Length - 1].Replace("Element", "");

            var sl = new ScriptLine
            {
                CharacterDialogId = characterDialog.Id,
                ProjectId = se.ProjectId,
                Line = se.FountainRawData,
                SequenceNumber = se.SequenceNumber,
                LineType = type,
                RecordingCount = characterDialog.RecordingCount
            };

            if (scene != null)
            {
                sl.SceneId = scene.Id;

                if (scene.SceneNumber != null)
                {
                    sl.SceneNumber = scene.SceneNumber;
                }
                else
                {
                    sl.SceneNumber = Convert.ToString(scene.SequenceNumber);
                }
            }

            return sl;
        }

        private ScriptLine BuildScriptLine(
            ScriptElement scriptElement,
            Dictionary<Guid, UIScene> scenes = null)
        {
            var parts = scriptElement.FountainElementType.Split(".".ToCharArray());
            var type = parts[parts.Length - 1].Replace("Element", "");

            var sl = new ScriptLine
            {
                CharacterDialogId = null,
                ProjectId = scriptElement.ProjectId,
                Line = scriptElement.FountainRawData,
                SequenceNumber = scriptElement.SequenceNumber,
                LineType = type,
                RecordingCount = 0
            };

            return sl;
        }

        public IEnumerable<ScriptLine> GetPreviousScriptLinesByCharacter(
            Guid userPersonId,
            Guid projectId,
            Guid characterId,
            int currentScriptSequenceNumber,
            int limit)
        {
            Dictionary<Guid, UIScene> scenes = new Dictionary<Guid, UIScene>();

            if (HasAccessToProject(userPersonId, projectId))
            {
                var output = new List<ScriptLine>();
                var characterDialogs = _scriptService.GetPreviousCharacterDialogsByCharacter(characterId, currentScriptSequenceNumber, limit);

                foreach (var characterDialog in characterDialogs)
                {
                    var sl = BuildScriptLine(characterDialog, scenes);
                    output.Add(sl);
                }

                return output.OrderBy(sl => sl.SequenceNumber);
            }

            throw new UnauthorizedAccessException();
        }

        public IEnumerable<ScriptLine> GetScriptLineContext(
            Guid userPersonId,
            Guid projectId,
            Guid characterDialogId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                Guid sceneId = Guid.Empty;
                var focusCD = _scriptService.GetCharacterDialog(characterDialogId);
                if (focusCD.SceneId.HasValue)
                {
                    sceneId = focusCD.SceneId.Value;
                }

                var scene = _scriptService.GetScene(sceneId);
                
                int start = focusCD.ScriptSequenceNumber - 4;
                int end = focusCD.ScriptSequenceNumber + 4;
                
                var elements = _scriptService.GetScriptElements(projectId, start, end);

                var output = new List<ScriptLine>();

                foreach (var se in elements)
                {
                    if (se.Id.Equals(focusCD.ScriptElementId))
                    {
                        output.Add(BuildScriptLine(focusCD, null, se));
                    }
                    else
                    {
                        output.Add(BuildScriptLine(se));
                    }
                }

                return output.OrderBy(sl => sl.SequenceNumber);
            }

            throw new UnauthorizedAccessException();
        }

        public Guid? GetNextLineId(
            Guid userPersonId,
            Guid projectId,
            Guid characterDialogId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                Guid sceneId = Guid.Empty;
                var focusCd = _scriptService.GetCharacterDialog(characterDialogId);

                var next = _scriptService.GetNextCharacterDialogByCharacter(
                    focusCd.CharacterId,
                    focusCd.ScriptSequenceNumber);

                return next?.Id;
            }

            throw new UnauthorizedAccessException();
        }

        public Guid? GetPreviousLineId(
            Guid userPersonId,
            Guid projectId,
            Guid characterDialogId)
        {
            if (HasAccessToProject(userPersonId, projectId))
            {
                Guid sceneId = Guid.Empty;
                var focusCd = _scriptService.GetCharacterDialog(characterDialogId);

                var previous = _scriptService.GetPreviousCharacterDialogByCharacter(
                    focusCd.CharacterId,
                    focusCd.ScriptSequenceNumber);

                return previous?.Id;
            }

            throw new UnauthorizedAccessException();
        }
    }
}
