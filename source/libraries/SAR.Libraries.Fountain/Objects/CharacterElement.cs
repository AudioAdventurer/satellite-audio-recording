using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using SAR.Libraries.Fountain.Constants;

namespace SAR.Libraries.Fountain.Objects
{
    public class CharacterElement : Element
    {
        const string extensions = @"\((.*?)\)";
        const string name = @"\A([A-Za-z0-9\s]+)(?:\(.+\))?";

        public CharacterElement(string rawData)
            : base(rawData)
        {
            var nameMatch = Regex.Match(rawData, name);
            if (nameMatch.Groups.Count > 0)
            {
                Name = nameMatch.Groups[1].Value.Trim();
            }
            
            Extensions = new List<string>();

            var matches = Regex.Matches(rawData, extensions);

            foreach (Match match in matches)
            {
                Extensions.Add(match.Groups[0].Value);
            }
        }

        public string Name { get; set; }

        public List<string> Extensions { get; set; }
    }
}
