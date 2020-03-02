using SAR.Libraries.Database.Objects;

namespace SAR.Modules.Script.Objects
{
    public class Person : AbstractDbObject
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string PhoneNumber { get; set; }
        
        //TODO Add
        //Twitter
        //Email - Contact vs login
        //etc.

        public string Name => (GivenName + " " + FamilyName).Trim();
    }
}
