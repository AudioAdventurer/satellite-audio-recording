using System;
using System.Collections.Generic;
using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Script.Objects
{
    public class Element : AbstractDbObject
    {
        public Element()
        {
            Content = new Content();
            CharacterIds = new List<Guid>();
        }

        public Guid SceneId { get; set; }

        public int SequenceNumber { get; set; }

        public string Type { get; set; }

        public Content Content { get; set; }

        public List<Guid> CharacterIds { get; set; }
    }
}
