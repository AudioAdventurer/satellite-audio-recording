using System;

namespace SAR.Libraries.Audio.Objects
{
    public class AudioTag
    {
        /// <summary>
        /// Scene number
        /// </summary>
        public int SceneNumber { get; set; }
        
        /// <summary>
        /// Line number resets for each scene
        /// </summary>
        public int LineNumber { get; set; }
        
        public DateTime RecordedOn { get; set; }
        
        public string Line { get; set; }
        
        public string Performer { get; set; }
    }
}