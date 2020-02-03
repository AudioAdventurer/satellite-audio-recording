using System;
using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Script.Objects
{
    public class Recording : AbstractDbObject
    {
        public Guid ProjectId { get; set; }

        public Guid CharacterDialogId { get; set; }

        public Guid PerformerPersonId { get; set; }

        public int SequenceNumber { get; set; }

        public DateTime RecordedOn { get; set; }

        public int SampleRate { get; set; }

        public long Duration { get; set; }
    }
}
