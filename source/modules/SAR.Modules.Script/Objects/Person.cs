using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Script.Objects
{
    public class Person : AbstractDbObject
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
    }
}
