using System;
using SAR.Libraries.Fountain.Objects;

namespace SAR.Libraries.Fountain.Factories
{
    public static class ElementFactory
    {
        public static Element BuildElement(
            string fountainElementType,
            string fountainElementRawData)
        {
            if (fountainElementType.Equals(typeof(SceneElement).FullName))
            {
                return new SceneElement(fountainElementRawData);
            }

            throw new NotSupportedException(fountainElementType);
        }
    }
}
