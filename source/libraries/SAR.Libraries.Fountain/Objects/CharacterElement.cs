using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SAR.Libraries.Fountain.Objects
{
    public class CharacterElement : Element
    {
        const string extensions = @"\((.*?)\)";

        public CharacterElement(string rawData)
            : base(rawData)
        {
            Extensions = new List<string>();

            var matches = Regex.Matches(rawData, extensions);

            foreach (Match match in matches)
            {
                Extensions.Add(match.Groups[0].Value);
            }
        }

        public List<string> Extensions { get; set; }
    }
}
