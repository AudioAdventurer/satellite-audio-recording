using System;
using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Script.Objects
{
    public class Scene : AbstractDbObject
    {
        public Guid? ScriptElementId { get; set; }
        public int Number { get; set; }
        public int ScriptSequenceNumber { get; set; }
        
        public int SceneEndSequenceNumber { get; set; }
        public Guid ProjectId { get; set; }
    }
}
