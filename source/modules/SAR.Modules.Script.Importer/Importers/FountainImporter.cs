using System;
using SAR.Modules.Script.Services;
using SAR.Libraries.Fountain.Parser;

namespace SAR.Modules.Script.Importer.Importers
{
    public class FountainImporter
    {
        private readonly ScriptService _scriptService;

        public FountainImporter(
            ScriptService scriptService)
        {
            _scriptService = scriptService;
        }

        public void Import(Guid projectId, string script)
        {
            var elements = FountainParser.Parse(script);

            foreach (var element in elements)
            {
                
            }
        }
    }
}
