using System;
using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Script.Objects
{
    public class Character : AbstractDbObject
    {
        public string Name { get; set; }

        public Guid ProjectId { get; set; }

        public Guid? PerformerPersonId { get; set; }

        public int FirstDialogSequenceNumber { get; set; }

        public int LastDialogSequenceNumber { get; set; }
    }
}
