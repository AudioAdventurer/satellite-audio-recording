﻿using System.Collections.Generic;
using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Script.Objects
{
    public class Project : AbstractDbObject
    {
        public Project()
        {
            ScriptProperties = new Dictionary<string, string>();
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public string ScriptTitle { get; set; }
        public Dictionary<string, string> ScriptProperties { get; set; }
    }
}
