using System;
using System.Collections.Generic;
using SAR.Libraries.Fountain.Objects;
using SAR.Modules.Script.Services;
using SAR.Libraries.Fountain.Parser;
using SAR.Modules.Script.Importer.Objects;
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
                        if (!characters.ContainsKey(ce.Name))
                        {
                            Character c = new Character
                            {
                                Name = ce.Name,
                                ProjectId = projectId
                            };

                            characters.Add(ce.Name, c);
                        }
                    }
                    else if (element is SceneElement se)
                    {
                        project.Scenes.Add(scriptElement.Id);
                    }
                }
            }

            //Clear any old information
            _scriptService.DeleteProjectScript(projectId);

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
                    project.ScriptProperties.Add(prop, element.Properties[prop]);
                }
            }
        }
    }
}
