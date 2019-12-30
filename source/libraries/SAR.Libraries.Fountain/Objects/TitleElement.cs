using System.Collections.Generic;

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

        }
    }
}
