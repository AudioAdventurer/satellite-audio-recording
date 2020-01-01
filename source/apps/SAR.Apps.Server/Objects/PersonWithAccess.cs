using System.Collections.Generic;
using SAR.Modules.Script.Objects;

namespace SAR.Apps.Server.Objects
{
    public class PersonWithAccess : Person
    {
        public PersonWithAccess()
        {
            AccessTypes = new List<string>();
        }

        public PersonWithAccess(Person p)
        {
            this.Id = p.Id;
            this.FamilyName = p.FamilyName;
            this.GivenName = p.GivenName;
            this.Email = p.Email;
            this.PhoneNumber = p.PhoneNumber;

            AccessTypes = new List<string>();
        }

        public List<string> AccessTypes { get; set; }
    }
}
