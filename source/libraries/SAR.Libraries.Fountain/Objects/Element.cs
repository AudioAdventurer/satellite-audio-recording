namespace SAR.Libraries.Fountain.Objects
{
    public abstract class Element
    {
        protected Element(string rawData)
        {
            RawData = rawData;
        }

        public string RawData { get; set; }
    }
}
