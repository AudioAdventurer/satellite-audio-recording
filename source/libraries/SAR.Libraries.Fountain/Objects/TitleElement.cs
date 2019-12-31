using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SAR.Libraries.Fountain.Constants;

namespace SAR.Libraries.Fountain.Objects
{
    public class TitleElement : Element
    {
        public TitleElement(string rawData)
            : base(rawData)
        {
            Properties = new Dictionary<string, string>();

            ParseData(rawData);
        }

        public Dictionary<string, string> Properties { get; set; }

        private void ParseData(string rawData)
        {
            var lines = rawData.Split("\n".ToCharArray());

            string openKey = null;
            StringBuilder openValue = new StringBuilder();


            foreach (var line in lines)
            {
                if (Regex.IsMatch(line, FountainRegEx.INLINE_DIRECTIVE_PATTERN))
                {
                    if (openKey != null)
                    {
                        Properties.Add(openKey, openValue.ToString());
                        openKey = null;
                        openValue = new StringBuilder();
                    }

                    var match = Regex.Match(line, FountainRegEx.INLINE_DIRECTIVE_PATTERN);

                    string key = match.Groups[match.Groups.Count - 2].Value;
                    string value = match.Groups[match.Groups.Count - 1].Value;

                    Properties.Add(key, value);
                }
                else if (Regex.IsMatch(line, FountainRegEx.MULTI_LINE_DIRECTIVE_PATTERN))
                {
                    if (openKey != null)
                    {
                        Properties.Add(openKey, openValue.ToString());
                        openKey = null;
                        openValue = new StringBuilder();
                    }

                    var match = Regex.Match(line, FountainRegEx.MULTI_LINE_DIRECTIVE_PATTERN);

                    openKey = match.Groups[match.Groups.Count-1].Value.Trim();
                }
                else if (Regex.IsMatch(line, FountainRegEx.MULTI_LINE_DATA_PATTERN))
                {
                    var match = Regex.Match(line, FountainRegEx.MULTI_LINE_DATA_PATTERN);

                    openValue.AppendLine(match.Groups[match.Groups.Count - 1].Value.Trim());
                }
            }

            if (openKey != null)
            {
                Properties.Add(openKey, openValue.ToString());
            }
        }
    }
}
