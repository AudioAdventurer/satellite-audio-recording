using System;
using SAR.Libraries.Audio.Objects;

namespace SAR.Libraries.Audio.Helpers
{
    public static class AudioTagHelper
    {
        public static void WriteTag(string filename, AudioTag audioTag)
        {
            using (var tFile = TagLib.File.Create(filename))
            {
                tFile.Tag.Title = audioTag.SceneNumber.ToString("00") 
                                  + "-" 
                                  + audioTag.LineNumber.ToString("00")
                                  + "-"
                                  + audioTag.RecordedOn.ToString("O");

                tFile.Tag.Performers = new[] {audioTag.Performer};
                tFile.Tag.Comment = audioTag.Line;
                tFile.Save();
            }
        }

        public static AudioTag ReadTag(string filename)
        {
            using (var tFile = TagLib.File.Create(filename))
            {
                var audioTag = new AudioTag();

                string temp = tFile.Tag.Title;
                if (temp != null)
                {
                    var parts = temp.Split("-".ToCharArray());
                    if (parts.Length == 3)
                    {
                        audioTag.SceneNumber = Convert.ToInt32(parts[0]);
                        audioTag.LineNumber = Convert.ToInt32(parts[1]);
                        audioTag.RecordedOn = Convert.ToDateTime(parts[2]);
                    }
                }
                audioTag.Performer = tFile.Tag.FirstPerformer;
                audioTag.Line = tFile.Tag.Comment;

                return audioTag;
            }
        }
    }
}