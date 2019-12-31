using System.Collections.Generic;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Importer.Objects
{
    public class SceneBinder
    {
        public SceneBinder()
        {
            Elements = new List<Element>();
        }

        public Scene Scene { get; set; }

        public List<Element> Elements { get; set; }
    }
}
