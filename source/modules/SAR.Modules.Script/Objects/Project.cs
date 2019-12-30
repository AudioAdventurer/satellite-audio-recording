using System;
using System.Collections.Generic;
using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Script.Objects
{
    public class Project : AbstractDbObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Locale { get; set; }

    }
}
