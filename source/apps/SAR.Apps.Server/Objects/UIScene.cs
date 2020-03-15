using System;

namespace SAR.Apps.Server.Objects
{
    public class UIScene
    {
        public Guid Id { get; set; }
        public int SequenceNumber { get; set; }
        public String InteriorExterior { get; set; }
        public string Location { get; set; }
        public string TimeOfDay { get; set; }
        public string SceneNumber { get; set; }
        public int ScriptPosition { get; set; }
        public int ScriptEndPosition { get; set; }
    }
}
