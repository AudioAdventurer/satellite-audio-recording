using System;

namespace SAR.Apps.Server.Objects
{
    public class ScriptLine
    {
        public int SequenceNumber { get; set; }

        public Guid ProjectId { get; set; }

        public string LineType { get; set; }

        public string Line { get; set; }
    }
}
