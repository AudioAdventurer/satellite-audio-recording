using System;

namespace SAR.Apps.Server.Objects
{
    public class ScriptLine
    {
        public Guid? CharacterDialogId { get; set; }

        public int SequenceNumber { get; set; }

        public Guid ProjectId { get; set; }

        public string LineType { get; set; }

        public string Line { get; set; }

        public Guid SceneId { get; set; }

        public string SceneNumber { get; set; }

        public int RecordingCount { get; set; }
    }
}
