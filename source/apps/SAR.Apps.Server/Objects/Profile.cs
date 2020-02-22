using System;

namespace SAR.Apps.Server.Objects
{
    public class Profile
    {
        public Guid PersonId { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string PhoneNumber { get; set; }
    }
}