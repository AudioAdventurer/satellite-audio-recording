using System.Collections.Generic;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Importer.Objects
{
    public class ProjectBinder
    {
        public ProjectBinder()
        {
            SceneBinders = new List<SceneBinder>();
            Characters = new Dictionary<string, Character>();
        }

        public Project Project { get; set; }

        public List<SceneBinder> SceneBinders { get; set; }

        public Dictionary<string, Character> Characters { get; set; }
    }
}
