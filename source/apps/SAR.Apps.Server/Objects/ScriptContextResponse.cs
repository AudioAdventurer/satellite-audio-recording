using System;
using System.Collections.Generic;
using System.Text;

namespace SAR.Apps.Server.Objects
{
    public class ScriptContextResponse
    {
        public ScriptContextResponse()
        {
            Context = new List<ScriptLine>();
        }

        public List<ScriptLine> Context { get; set; }

        public Guid? PreviousLine { get; set; }
        public Guid? NextLine { get; set; }
    }
}
