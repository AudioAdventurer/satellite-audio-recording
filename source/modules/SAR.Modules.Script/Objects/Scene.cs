using System;
using System.Collections.Generic;
using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Script.Objects
{
    public class Scene : AbstractDbObject
    {
        public Scene()
        {
            Characters = new List<Guid>();
            Sounds = new List<Sound>();
        }

        public int SequenceNumber { get; set; }

        public string Context { get; set; }

        public string Setting { get; set; }

        public string Sequence { get; set; }

        public string Description { get; set; }

        public Guid ProjectId { get; set; }

        public List<Guid> Characters { get; set; }

        public List<Sound> Sounds { get; set; }
    }
}
