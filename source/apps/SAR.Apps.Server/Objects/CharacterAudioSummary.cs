using System;

namespace SAR.Apps.Server.Objects
{
    public class CharacterAudioSummary
    {
        public Guid CharacterId { get; set; }
        
        public string Name { get; set; }
        
        public int Lines { get; set; }
        
        public int LinesWithAudio { get; set; }
    }
}