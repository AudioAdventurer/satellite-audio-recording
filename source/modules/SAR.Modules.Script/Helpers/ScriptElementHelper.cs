using SAR.Libraries.Fountain.Factories;
using SAR.Libraries.Fountain.Objects;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Helpers
{
    public static class ScriptElementHelper
    {
        public static Element ToFountain(this ScriptElement scriptElement)
        {
            return ElementFactory.BuildElement(
                scriptElement.FountainElementType, 
                scriptElement.FountainRawData);
        }
    }
}
