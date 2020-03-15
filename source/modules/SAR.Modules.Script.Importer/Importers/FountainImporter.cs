using System;
using System.Collections.Generic;
using SAR.Libraries.Fountain.Objects;
using SAR.Modules.Script.Services;
using SAR.Libraries.Fountain.Parser;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Importer.Importers
{
    public class FountainImporter
    {
        private readonly ScriptService _scriptService;

        public FountainImporter(
            ScriptService scriptService)
        {
            _scriptService = scriptService;
        }

        public void Import(Guid projectId, string script)
        {
            var elements = FountainParser.Parse(script);

            var project = _scriptService.GetProject(projectId);
            var characters = new Dictionary<string, Character>();
            var scriptElements = new List<ScriptElement>();
            var characterDialogs = new List<CharacterDialog>();
            var scenes = new List<Scene>();

            Character lastCharacter = null;
            Scene currentScene = null;
            int sceneNumber = 1;

            int sequenceNumber = 0;
            foreach (var element in elements)
            {
                if (element is TitleElement t)
                {
                    ProcessTitle(project, t);
                }
                else
                {
                    var type = element.GetType().FullName;

                    ScriptElement scriptElement = new ScriptElement
                    {
                        SequenceNumber = sequenceNumber,
                        ProjectId = projectId,
                        FountainElementType = type,
                        FountainRawData = element.RawData
                    };
                    scriptElements.Add(scriptElement);
                    sequenceNumber++;

                    //Handle some special types
                    if (element is CharacterElement ce)
                    {
                        Character c;

                        if (characters.ContainsKey(ce.Name))
                        {
                            c = characters[ce.Name];
                        }
                        else
                        {
                            c = new Character
                            {
                                Name = ce.Name,
                                ProjectId = projectId,
                                FirstDialogSequenceNumber = Int32.MaxValue,
                                LastDialogSequenceNumber = Int32.MinValue
                            };

                            characters.Add(ce.Name, c);
                        }

                        lastCharacter = c;
                    }
                    else if (element is SceneElement se)
                    {
                        Scene s = new Scene
                        {
                            ProjectId = projectId,
                            Number = sceneNumber,
                            ScriptElementId = scriptElement.Id,
                            ScriptSequenceNumber = scriptElement.SequenceNumber
                        };

                        scenes.Add(s);

                        currentScene = s;

                        sceneNumber++;
                    }
                    else if (element is DialogueElement de)
                    {
                        if (lastCharacter != null)
                        {
                            CharacterDialog cd = new CharacterDialog
                            {
                                CharacterId = lastCharacter.Id,
                                ProjectId = projectId,
                                ScriptElementId = scriptElement.Id,
                                ScriptSequenceNumber = scriptElement.SequenceNumber,
                                RecordingCount = 0
                            };

                            if (currentScene != null)
                            {
                                cd.SceneId = currentScene.Id;
                            }

                            characterDialogs.Add(cd);
                        }
                    }
                }
            }

            //Clear any old information
            _scriptService.DeleteProjectScript(projectId);

            var maxSequenceNumber = sequenceNumber - 1;
            
            //Force a scene zero into script if script doesn't start with a scene
            var firstScene = scenes[0];
            if (firstScene.ScriptSequenceNumber != 0)
            {
                var sceneZero = new Scene
                {
                    ProjectId = projectId, 
                    ScriptSequenceNumber = -1, 
                    Number = 0, 
                    ScriptElementId = null
                };
                
                scenes.Insert(0, sceneZero);
            }
            
            //Set scene end points for easier loading of entire scene
            int sceneCount = scenes.Count;

            for (int i = 0; i < sceneCount-1; i++)
            {
                var current = scenes[i];
                var next = scenes[i + 1];

                current.SceneEndSequenceNumber = next.ScriptSequenceNumber - 1;
            }

            var last = scenes[sceneCount - 1];
            last.SceneEndSequenceNumber = maxSequenceNumber;
            

            //Find first and last lines for each character
            foreach (var key in characters.Keys)
            {
                var character = characters[key];

                foreach (var characterDialog in characterDialogs)
                {
                    if (characterDialog.CharacterId == character.Id)
                    {
                        if (characterDialog.ScriptSequenceNumber < character.FirstDialogSequenceNumber)
                        {
                            character.FirstDialogSequenceNumber = characterDialog.ScriptSequenceNumber;
                        }

                        if (characterDialog.ScriptSequenceNumber > character.LastDialogSequenceNumber)
                        {
                            character.LastDialogSequenceNumber = characterDialog.ScriptSequenceNumber;
                        }
                    }
                }
            }

            //Save the script and child data
            _scriptService.Save(project);

            foreach (var key in characters.Keys)
            {
                var character = characters[key];
                _scriptService.Save(character);
            }

            foreach (var scriptElement in scriptElements)
            {
                _scriptService.Save(scriptElement);
            }

            foreach (var scene in scenes)
            {
                _scriptService.Save(scene);
            }

            foreach (var characterDialog in characterDialogs)
            {
                _scriptService.Save(characterDialog);
            }
        }

        private void ProcessTitle(
            Project project, 
            TitleElement element)
        {
            foreach (var prop in element.Properties.Keys)
            {
                if (prop.Equals("title", StringComparison.InvariantCultureIgnoreCase))
                {
                    project.ScriptTitle = element.Properties[prop];
                }
                else
                {
                    if (project.ScriptProperties.ContainsKey(prop))
                    {
                        project.ScriptProperties[prop] = element.Properties[prop];
                    }
                    else
                    {
                        project.ScriptProperties.Add(prop, element.Properties[prop]);
                    }
                }
            }
        }
    }
}
