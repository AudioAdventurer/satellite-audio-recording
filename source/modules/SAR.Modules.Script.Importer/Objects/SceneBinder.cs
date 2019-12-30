using System;
using System.Collections.Generic;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Importer.Objects
{
    public class SceneBinder
    {
        public SceneBinder()
        {
            CharacterIds = new List<Guid>();
            Elements = new List<Element>();
        }

        public List<Guid> CharacterIds { get; set; }

        public Scene Scene { get; set; }

        public List<Element> Elements { get; set; }
    }
}
