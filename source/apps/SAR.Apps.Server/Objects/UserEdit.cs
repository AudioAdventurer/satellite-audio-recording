using System;

namespace SAR.Apps.Server.Objects
{
    public class UserEdit
    {
        public Guid UserId { get; set; }
        public Guid PersonId { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string PhoneNumber { get; set; }
    }
}