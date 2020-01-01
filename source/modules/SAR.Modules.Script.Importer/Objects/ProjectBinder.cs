using System;
using System.Collections.Generic;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Importer.Objects
{
    public class ProjectBinder
    {
        public ProjectBinder()
        {
            Elements = new List<ScriptElement>();
            Characters = new Dictionary<string, Character>();
        }

        public Project Project { get; set; }

        public List<ScriptElement> Elements { get; set; }

        public Dictionary<string, Character> Characters { get; set; }
    }
}
