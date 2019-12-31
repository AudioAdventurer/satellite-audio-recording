using System;
using SAR.Libraries.Fountain.Objects;
using SAR.Modules.Script.Services;
using SAR.Libraries.Fountain.Parser;
using SAR.Modules.Script.Constants;
using SAR.Modules.Script.Importer.Objects;
using SAR.Modules.Script.Objects;
using Element = SAR.Libraries.Fountain.Objects.Element;

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

            _scriptService.DeleteProjectScript(projectId);

            int sceneNumber = 0;

            var project = _scriptService.GetProject(projectId);
            var projectBinder = new ProjectBinder
            {
                Project = project
            };

            var sceneBinder = new SceneBinder
            {
                Scene = new Scene()
            };
            projectBinder.SceneBinders.Add(sceneBinder);

            foreach (var element in elements)
            {
                if (element is TitleElement t)
                {
                    ProcessTitle(projectBinder, t);
                }
                else if (element is ActionElement ae)
                {
                    ProcessAction(projectBinder, sceneNumber, ae);
                }
                else if (element is BoneyardElement be)
                {
                    ProcessBoneyard(projectBinder, sceneNumber, be);
                }
                else if (element is CharacterElement ce)
                {
                    ProcessCharacter(projectBinder, sceneNumber, ce);
                }
                else if (element is CommentElement coe)
                {
                    ProcessComment(projectBinder, sceneNumber, coe);
                }
                else if (element is DialogueElement de)
                {
                    ProcessDialog(projectBinder, sceneNumber, de);
                }
                else if (element is PageBreakElement pbe)
                {
                    //DO NOTHING
                }
                else if (element is ParentheticalElement pe)
                {
                    ProcessParenthetical(projectBinder, sceneNumber, pe);
                }
                else if (element is SceneElement se)
                {
                    var scene = new Scene();

                    var sb = new SceneBinder
                    {
                        Scene = scene
                    };

                    projectBinder.SceneBinders.Add(sb);
                    sceneNumber++;

                    ProcessScene(projectBinder, sceneNumber, se);
                }
                else if (element is SectionHeadingElement she)
                {
                    ProcessSectionHeader(projectBinder, sceneNumber, she);
                }
                else if (element is SynopsisElement syne)
                {
                    ProcessSynopsis(projectBinder, sceneNumber, syne);
                }
                else if (element is TransitionElement te)
                {
                    ProcessTransition(projectBinder, sceneNumber, te);
                }
                else
                {
                    throw new Exception($"Unknown element found - {element.GetType()}");
                }
            }
        }



        private void ProcessAction(
            ProjectBinder projectBinder,
            int sceneNumber,
            ActionElement element)
        {
            var sceneBinder = projectBinder.SceneBinders[sceneNumber];

            Script.Objects.Element e = new Script.Objects.Element
            {
                SceneId = sceneBinder.Scene.Id,
                Type = ElementTypes.Action,
                Content = element.RawData
            };

            sceneBinder.Elements.Add(e);
        }

        private void ProcessBoneyard(
            ProjectBinder projectBinder,
            int sceneNumber,
            BoneyardElement element)
        {
            //Do nothing for now
        }

        private void ProcessCharacter(
            ProjectBinder projectBinder,
            int sceneNumber,
            CharacterElement element)
        {
            var sceneBinder = projectBinder.SceneBinders[sceneNumber];

            Script.Objects.Element e = new Script.Objects.Element
            {
                SceneId = sceneBinder.Scene.Id,
                Type = ElementTypes.Character,
                Content = element.RawData
            };
            sceneBinder.Elements.Add(e);

            Character c;
            if (projectBinder.Characters.ContainsKey(element.Name))
            {
                c = projectBinder.Characters[element.Name];
            }
            else
            {
                c = new Character
                {
                    Name = element.Name,
                    ProjectId = projectBinder.Project.Id
                };

                projectBinder.Characters.Add(element.Name, c);
            }

            if (!sceneBinder.Scene.Characters.Contains(c.Id))
            {
                sceneBinder.Scene.Characters.Add(c.Id);
            }
        }

        private void ProcessComment(
            ProjectBinder projectBinder,
            int sceneNumber,
            CommentElement element)
        {
            var sceneBinder = projectBinder.SceneBinders[sceneNumber];

            Script.Objects.Element e = new Script.Objects.Element
            {
                SceneId = sceneBinder.Scene.Id,
                Type = ElementTypes.Comment,
                Content = element.RawData
            };

            sceneBinder.Elements.Add(e);
        }

        private void ProcessDialog(
            ProjectBinder projectBinder,
            int sceneNumber,
            DialogueElement element)
        {
            var sceneBinder = projectBinder.SceneBinders[sceneNumber];

            Script.Objects.Element e = new Script.Objects.Element
            {
                SceneId = sceneBinder.Scene.Id,
                Type = ElementTypes.Dialogue,
                Content = element.RawData
            };

            sceneBinder.Elements.Add(e);
        }

        private void ProcessParenthetical(
            ProjectBinder projectBinder,
            int sceneNumber,
            ParentheticalElement element)
        {

        }

        private void ProcessScene(
            ProjectBinder projectBinder,
            int sceneNumber,
            SceneElement element)
        {

        }

        private void ProcessSectionHeader(
            ProjectBinder projectBinder,
            int sceneNumber,
            SectionHeadingElement element)
        {

        }

        public void ProcessSynopsis(
            ProjectBinder projectBinder,
            int sceneNumber,
            SynopsisElement element)
        {

        }

        public void ProcessTransition(
            ProjectBinder projectBinder,
            int sceneNumber,
            TransitionElement element)
        {

        }

        private void ProcessTitle(
            ProjectBinder projectBinder, 
            TitleElement element)
        {
            foreach (var prop in element.Properties.Keys)
            {
                if (prop.Equals("title", StringComparison.InvariantCultureIgnoreCase))
                {
                    projectBinder.Project.ScriptTitle = element.Properties[prop];
                }
                else
                {
                    projectBinder.Project.ScriptProperties.Add(prop, element.Properties[prop]);
                }
            }
        }
    }
}
