using System;
using System.Collections.Generic;

namespace SAR.Apps.Server.Objects
{
    public class SceneAudioSummary
    {
        public SceneAudioSummary()
        {
            CharacterAudio = new Dictionary<Guid, CharacterAudioSummary>();
        }
        
        public int Number { get; set; }
        
        public string Description { get; set; }
        
        public int Lines { get; set; }
        
        public int LinesWithAudio { get; set; }
        
        public Dictionary<Guid, CharacterAudioSummary> CharacterAudio { get; set; }
    }
}