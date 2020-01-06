using System;
using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Script.Objects
{
    public class CharacterDialog : AbstractDbObject
    {
        public Guid ScriptElementId { get; set; }

        public Guid CharacterId { get; set; }

        public Guid ProjectId { get; set; }

        public Guid? SceneId { get; set; }

        public int ScriptSequenceNumber { get; set; }

        public int RecordingCount { get; set; }
    }
}
