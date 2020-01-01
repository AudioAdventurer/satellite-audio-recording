using System;
using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Script.Objects
{
    public class ScriptElement : AbstractDbObject
    {
        public int SequenceNumber { get; set; }

        public Guid ProjectId { get; set; }

        public string FountainElementType { get; set; }

        public string FountainRawData { get; set; }
    }
}
